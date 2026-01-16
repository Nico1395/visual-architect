<script setup lang="ts">
import Icon from '@/components/Icon.vue';
import ButtonGroup from '@/components/ui/button-group/ButtonGroup.vue';
import Button from '@/components/ui/button/Button.vue';
import { useI18n } from "vue-i18n"
import { inject, ref, type ComputedRef } from 'vue'
import type { DesignProjectDtoV1 } from '@/persistence/dtos/design-project.dtos';
import DesignProjectTaskFilter from './DesignProjectTaskFilter.vue';
import DesignProjectTaskItem from './DesignProjectTaskItem.vue';
import { useDesignProjectStore } from '@/persistence/stores/design-project.store';
import { useRouter } from 'vue-router';
import DesignTaskFormDialog from '@/components/design-projects/DesignTaskFormDialog.vue';

const { t } = useI18n();
const router = useRouter()
const designProjectStore = useDesignProjectStore()
const taskFormDialogOpened = ref(false)

const project = inject<ComputedRef<DesignProjectDtoV1 | undefined>>('design-project')
if (!project) {
  throw new Error('DesignProject not provided')
}

function openTaskFormDialog() {
    taskFormDialogOpened.value = true
}

function onTaskFormDialogSubmitted(result: { taskNumber: number |null | undefined }) {
    if (!result.taskNumber ||!project || !project.value)
        return

    router.push({
        path: `/app/design-projects/${project.value.id}/tasks/${result.taskNumber}`
    })
}
</script>

<template>
    <div class="design-project-tasks">
        <div class="design-project-tasks-header">
            <div class="design-project-tasks-title">
                <h1>
                    {{ t('designprojects.tasks.title') }}
                </h1>
            </div>

            <div class="design-project-tasks-actions">
                <DesignProjectTaskFilter />

                <ButtonGroup>
                    <Button :disabled="designProjectStore.busy && project" @click="openTaskFormDialog">
                        <Icon icon="ai-plus" />

                        {{ t('designprojects.tasks.new') }}
                    </Button>
                </ButtonGroup>
            </div>
        </div>

        <div class="design-project-tasks-body">
            <div v-if="!project || project.designTasks?.length == 0" class="design-project-tasks-none">
                {{ t('designprojects.tasks.list.notasks') }}
            </div>

            <div v-else class="design-project-tasks-items">
                <DesignProjectTaskItem
                    v-for="task in project.designTasks"
                    :key="task.id"
                    :value="task"
                    :task="task" />
            </div>
        </div>
    </div>

    <DesignTaskFormDialog
        v-if="project"
        :projectId="project.id"
        v-model:opened="taskFormDialogOpened"
        @submitted="onTaskFormDialogSubmitted"
     />
</template>

<style>
.design-project-tasks {
    display: flex;
    flex-direction: column;
    gap: 1rem;

    .design-project-tasks-header {
        flex: none;
        display: flex;
        flex-direction: row;
        justify-content: space-between;
        align-items: center;
        gap: 1rem;
        flex-wrap: wrap;

        .design-project-tasks-title {
            > h1 {
                font-weight: 700;
                font-size: 20pt;
            }
        }

        .design-project-tasks-actions {
            display: flex;
            flex-direction: row;
            gap: 0.5rem;
        }
    }

    .design-project-tasks-body {
        .design-project-tasks-none {
            font-weight: 500;
            font-size: 10pt;
            color: var(--muted-foreground);
            text-align: center;
            margin-top: 1rem;
        }

        .design-project-tasks-items {
            display: flex;
            flex-direction: column;
            gap: 0.2rem;
        }
    }
}
</style>
