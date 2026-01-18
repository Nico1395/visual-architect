<script setup lang="ts">
import Icon from '@/components/Icon.vue';
import ButtonGroup from '@/components/ui/button-group/ButtonGroup.vue';
import Button from '@/components/ui/button/Button.vue';
import Input from '@/components/ui/input/Input.vue';
import { useDesignProjectStore } from '@/persistence/stores/design-project.store';
import { computed, reactive, ref, watch } from 'vue';
import { useI18n } from 'vue-i18n';
import { useRoute } from 'vue-router';
import DesignCodeEditor from './DesignCodeEditor.vue';
import MarkdownArea from '@/components/MarkdownArea.vue';
import { ResizableHandle, ResizablePanel, ResizablePanelGroup } from '@/components/ui/resizable';
import { Tabs, TabsContent, TabsList, TabsTrigger } from '@/components/ui/tabs';

const { t } = useI18n();
const route = useRoute()
const designProjectStore = useDesignProjectStore()

const projectId = computed(() => route.params.projectId as string)
const project = computed(() => designProjectStore.projects.find(p => p.id === projectId.value))
const taskNumber = computed(() => route.params.taskNumber as string)
const task = computed(() => project.value?.designTasks?.find(t => t.number.toString() === taskNumber.value))
const designId = computed(() => route.params.designId as string)
const design = computed(() => project.value?.designTasks?.find(t => t.number.toString() === taskNumber.value)?.designs?.find(d => d.id == designId.value))

const editingName = ref(false)
const form = reactive({
    name: "",
    descriptionPayload: "",
})
const originalForm = reactive({
    name: "",
    descriptionPayload: "",
})

function stopEditingName() {
    editingName.value = false
    form.name = originalForm.name
}

async function updateName() {
    // await saveChanges(designProjectStore.updateTask(projectId.value, parseInt(taskNumber.value), {
    //     name: form.name,
    //     descriptionPayload: originalForm.descriptionPayload,
    //     status: originalForm.status,
    // }))

    originalForm.name = form.name
    editingName.value = false
}

// watch(
//     () => projectId.value,
//     (id) => designProjectStore.getProjectById(id),
//     { immediate: true }
// )

watch([projectId, taskNumber, designId], async ([pid, tnum, did]) => {
    if (!pid || !tnum || !did)
        return

    if (!design.value)
        await designProjectStore.getDesignByProjectIdTaskNumberAndId(projectId.value, Number(taskNumber.value), designId.value)
    },
    { immediate: true }
)

watch(design, (d) => {
        if (!d)
            return

        Object.assign(originalForm, {
            name: design.value?.name,
            descriptionPayload: design.value?.descriptionPayload,
        })

        Object.assign(form, {
            name: design.value?.name,
            descriptionPayload: design.value?.descriptionPayload,
        })
    },
    { immediate: true }
)
</script>

<template>
    <div class="design-view">
        <div class="design-header">
            <div class="design-header-title">
                <RouterLink class="design-return-href" :to="`/app/design-projects/${projectId}/tasks/${taskNumber}`">
                    <Icon icon="ai-arrow-left" />

                    {{ task?.name }}
                </RouterLink>

                <ButtonGroup v-if="editingName" class="design-name-editor">
                    <Input
                        class="design-name-input"
                        v-model="form.name"
                        :placeholder="t('design.header.name.placeholder')"
                        :disabled="designProjectStore.busy"
                    />

                    <Button variant="outline" @click="stopEditingName" :disabled="designProjectStore.busy">
                        <Icon icon="ai-cross" />
                    </Button>

                    <Button @click="updateName">
                        <Icon icon="ai-check" />
                    </Button>
                </ButtonGroup>

                <div class="design-name" :disabled="designProjectStore.busy" v-else @click="editingName = true">
                    {{ design?.name }}

                    <Icon icon="ai-pencil" />
                </div>
            </div>

            <div class="design-header-actions">

            </div>
        </div>

        <ResizablePanelGroup direction="horizontal" class="design-body">
            <ResizablePanel class="design-sidebar-panel" :default-size="20">
                <Tabs class="design-sidebar-tabs" default-value="description">
                    <TabsList>
                        <TabsTrigger value="description">
                            Description
                        </TabsTrigger>

                        <TabsTrigger value="details">
                            Details
                        </TabsTrigger>
                    </TabsList>

                    <TabsContent value="description">
                        <MarkdownArea v-if="design" v-model="design.descriptionPayload" class="design-description-editor" />
                    </TabsContent>

                    <TabsContent value="details">
                        Details
                    </TabsContent>
                </Tabs>
            </ResizablePanel>

            <ResizableHandle class="design-editor-resize-handle" />

            <ResizablePanel class="design-editor-panel">
                <DesignCodeEditor v-if="design?.type == 0" v-model="design.payload" :disabled="designProjectStore.busy" />
            </ResizablePanel>
        </ResizablePanelGroup>
    </div>
</template>

<style>
#app:has(.design-view) {
    .footer {
        display: none;
    }
}

.content:has(.design-view) {
    margin: 0;
    overflow: hidden;
}

.design-view {
    height: 100%;
    padding: 1rem;
    display: flex;
    flex-direction: column;
    box-sizing: border-box;

    .design-header {
        display: flex;
        flex-direction: column;
        gap: 0.5rem;
        border-bottom: 1px solid var(--border);

        .design-return-href {
            font-size: 10pt;
            font-weight: 500;

            &:hover {
                text-decoration: 1px underline;
            }
        }

        .design-name-editor {
            width: 500px;

            .design-name-input {
                font-weight: 500;
                font-size: 12pt;
            }
        }

        .design-name {
            font-weight: 700;
            font-size: 16pt;
            width: fit-content;
            cursor: pointer;
            display: flex;
            flex-direction: row;
            align-items: center;
            gap: 0.5rem;

            &:hover {
                .icon {
                    display: unset;
                }
            }

            .icon {
                font-size: 13pt;
                display: none;
            }
        }
    }

    .design-body {
        .design-sidebar-panel {
            width: 365px;
            min-width: 365px;

            .design-sidebar-tabs {
                height: 100%;
                padding-top: 0.5rem;
                padding-right: 0.5rem;

                .design-description-editor {
                    height: 100%;

                    .markdown-editor {
                        height: 100%;
                    }
                }
            }
        }

        .design-editor-panel {
            overflow: hidden;

            .design-editor {
                height: 100%;
                padding-top: 0.5rem;
                padding-left: 0.5rem;
            }
        }
    }
}
</style>
