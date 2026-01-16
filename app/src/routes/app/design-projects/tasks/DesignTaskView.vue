<script setup lang="ts">
import Icon from '@/components/Icon.vue';
import ViewMargin from '@/components/layout/ViewMargin.vue';
import MarkdownArea from '@/components/MarkdownArea.vue';
import { useDesignProjectStore } from '@/persistence/stores/design-project.store';
import { computed, provide, reactive, ref, watch } from 'vue';
import { useI18n } from 'vue-i18n';
import { useRoute, useRouter } from 'vue-router';
import { toast } from 'vue-sonner';
import DesignTaskDesignItem from './DesignTaskDesignItem.vue';
import DesignTaskInfoItem from './DesignTaskInfoItem.vue';
import DesignTaskStatus from './DesignTaskStatus.vue';
import Button from '@/components/ui/button/Button.vue';
import DesignTaskMenu from './DesignTaskMenu.vue';
import Input from '@/components/ui/input/Input.vue';
import ButtonGroup from '@/components/ui/button-group/ButtonGroup.vue';

const { t } = useI18n();
const router = useRouter()
const route = useRoute()
const designProjectStore = useDesignProjectStore()

const projectId = computed(() => route.params.projectId as string)
const project = computed(() =>
    designProjectStore.projects.find(p => p.id === projectId.value)
)
const taskNumber = computed(() => route.params.taskNumber as string)
const task = computed(() =>
    project.value?.designTasks?.find(t => t.number.toString() === taskNumber.value)
)

const editingName = ref(false)
const originalForm = reactive({
    name: "",
    descriptionPayload: "",
    status: 0,
})
const form = reactive({
    name: "",
    descriptionPayload: "",
    status: 0,
})

function stopEditingName() {
    editingName.value = false
    form.name = originalForm.name
}

async function updateName() {
    await saveChanges(designProjectStore.updateTask(projectId.value, parseInt(taskNumber.value), {
        name: form.name,
        descriptionPayload: originalForm.descriptionPayload,
        status: originalForm.status,
    }))

    originalForm.name = form.name
    editingName.value = false
}

async function updateDescriptionPayload() {
    await saveChanges(designProjectStore.updateTask(projectId.value, parseInt(taskNumber.value), {
        name: originalForm.name,
        descriptionPayload: form.descriptionPayload,
        status: originalForm.status,
    }))

    originalForm.descriptionPayload = form.descriptionPayload
}

async function updateStatus(status: number) {
    await saveChanges(designProjectStore.updateTask(projectId.value, parseInt(taskNumber.value), {
        name: originalForm.name,
        descriptionPayload: originalForm.descriptionPayload,
        status: status
    }))

    originalForm.status = form.status
}

async function saveChanges(promise: Promise<void>) {
    toast.promise(promise, {
        loading: t('toasts.saving.loading'),
        success: t('toasts.saving.success'),
        error: t('toasts.saving.error'),
    });

    await promise;
}

async function deleteTask() {
    const promise = designProjectStore.deleteTask(projectId.value, parseInt(taskNumber.value))
    toast.promise(promise, {
        loading: t('toasts.saving.loading'),
        success: t('toasts.saving.success'),
        error: t('toasts.saving.error'),
    });

    await promise;

    router.push({
        path: `/app/design-projects/${projectId.value}/tasks`
    })
}

watch(
    () => projectId.value,
    (id) => designProjectStore.getProjectById(id),
    { immediate: true }
)

watch([projectId, taskNumber], async ([pid, tnum]) => {
    if (!pid || !tnum)
        return

    if (!task.value)
      await designProjectStore.getTaskByProjectIdAndNumber(pid, Number(tnum) )
  },
  { immediate: true }
)

watch(task, (t) => {
        if (!t)
            return

        Object.assign(originalForm, {
            id: task.value?.id,
            name: task.value?.name,
            descriptionPayload: task.value?.descriptionPayload,
            status: task.value?.status
        })

        Object.assign(form, {
            id: task.value?.id,
            name: task.value?.name,
            descriptionPayload: task.value?.descriptionPayload,
            status: task.value?.status
        })
    },
    { immediate: true }
)

provide('design-task', task)
</script>

<template>
    <ViewMargin class="design-task">
        <div class="design-task-header">
            <div class="design-task-title">
                <RouterLink class="design-task-return-href" :to="`/app/design-projects/${projectId}/tasks`">
                    <Icon icon="ai-arrow-left" />

                    {{ project?.name }}
                </RouterLink>

                <ButtonGroup v-if="editingName" class="design-task-name-editor">
                    <Input
                        class="design-task-name-input"
                        v-model="form.name"
                        :placeholder="t('designTask.header.name.placeholder')"
                        :disabled="designProjectStore.busy"
                    />

                    <Button variant="outline" @click="stopEditingName" :disabled="designProjectStore.busy">
                        <Icon icon="ai-cross" />
                    </Button>

                    <Button @click="updateName">
                        <Icon icon="ai-check" />
                    </Button>
                </ButtonGroup>

                <div class="design-task-name" :disabled="designProjectStore.busy" v-else @click="editingName = true">
                    {{ task?.name }}

                    <Icon icon="ai-pencil" />
                </div>
            </div>

            <div class="design-task-row">
                <div class="design-task-info">
                    <DesignTaskInfoItem>
                        <template #name>
                            {{ t('designTask.status.name') }}
                        </template>

                        <template #content>
                            <DesignTaskStatus v-if="task" :task="task" @status:selected="updateStatus" />
                        </template>
                    </DesignTaskInfoItem>
                </div>

                <DesignTaskMenu :busy="designProjectStore.busy" @deleted="deleteTask" />
            </div>
        </div>

        <hr />

        <div class="design-task-description">
            <div class="design-task-description-title">
                {{ t('designTask.description.title') }}
            </div>

            <MarkdownArea v-model="form.descriptionPayload" @save="updateDescriptionPayload" :placeholder="t('designprojects.overview.description.none')">
                <template #title>
                    {{ t('actions.edit') }}
                </template>
            </MarkdownArea>
        </div>

        <hr />

        <div class="design-task-designs">
            <div class="design-task-designs-header">
                <div class="design-task-designs-title">
                    {{ t('designTask.designs.title') }}
                </div>

                <div class="design-task-designs-actions">
                    <Button size="sm">
                        <Icon icon="ai-plus" />

                        {{ t('actions.new') }}
                    </Button>
                </div>
            </div>

            <div v-if="task?.designs && task.designs.length > 0" class="design-task-designs-grid">
                <DesignTaskDesignItem
                    v-for="design in task.designs"
                    :key="design.id"
                    :to="`/app/design-projects/${projectId}/tasks/${taskNumber}/${design.id}`"
                    :name="design.name"
                />
            </div>

            <div v-else class="design-task-designs-no-designs">
                {{ t('designTask.designs.noDesigns') }}
            </div>
        </div>
    </ViewMargin>
</template>

<style>
.design-task {
    display: flex;
    flex-direction: column;
    gap: 1rem;

    .design-task-header {
        display: flex;
        flex-direction: column;
        gap: 0.5rem;

        .design-task-title {
            .design-task-return-href {
                font-size: 10pt;
                font-weight: 500;
            }

            .design-task-name-editor {
                width: 100%;

                .design-task-name-input {
                    font-weight: 500;
                    font-size: 12pt;
                }
            }

            .design-task-name {
                font-weight: 700;
                font-size: 20pt;
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

        .design-task-row {
            display: flex;
            flex-direction: row;
            flex-wrap: wrap;
            justify-content: space-between;
            align-items: center;

            .design-task-info {
                display: flex;
                flex-direction: row;
                flex-wrap: wrap;
                align-items: center;
                gap: 2rem;
            }
        }
    }

    .design-task-description {
        .design-task-description-title {
            font-weight: 600;
            font-size: 12pt;
        }
    }

    .design-task-designs {
        .design-task-designs-header {
            display: flex;
            flex-direction: row;
            justify-content: space-between;
            align-items: center;

            .design-task-designs-title {
                font-weight: 600;
                font-size: 12pt;
            }
        }

        .design-task-designs-grid {
            display: grid;
            grid-template-columns: repeat(auto-fill, minmax(200px, 1fr));
            gap: 1.5rem;
        }

        .design-task-designs-no-designs {
            font-weight: 500;
            font-size: 10pt;
            color: var(--muted-foreground);
            text-align: center;
            margin: 1rem 0;
        }
    }
}
</style>
