<script setup lang="ts">
import Badge from '@/components/ui/badge/Badge.vue';
import {
  Item,
  ItemContent,
} from '@/components/ui/item'
import type { DesignTaskDtoV1 } from '@/persistence/dtos/design-project.dtos';
import { useI18n } from 'vue-i18n';

const { t, d } = useI18n();
const props = defineProps<{
    task: DesignTaskDtoV1
}>()

function getStatusClass() {
    switch (props.task.status) {
        case 0: return "todo"
        case 1: return "progress"
        case 2: return "completed"
    }
}

function getDescription() {
    const createdAt = new Date(props.task.createdAt)
    const updatedAt = new Date(props.task.createdAt)

    return `#${props.task.number} • ${t('designprojects.tasks.list.task.createdat')} ${d(createdAt)} • ${t('designprojects.tasks.list.task.updatedat')} ${d(updatedAt)}`
}
</script>

<template>
    <Item variant="outline">
        <ItemContent class="design-project-task-item">
            <div class="design-project-task-item-header">
                <RouterLink class="design-project-task-item-link" :to="`/app/design-projects/${props.task.projectId}/${props.task.number}`">
                    {{ props.task.name }}
                </RouterLink>

                <Badge :class="`design-project-task-item-status ${getStatusClass()}`">
                    {{ t(`designprojects.tasks.list.task.status.${getStatusClass()}`) }}
                </Badge>
            </div>

            <div class="design-project-task-item-status">

            </div>

            <div class="design-project-task-item-description">
                {{ getDescription() }}
            </div>
        </ItemContent>
    </Item>
</template>

<style>
.design-project-task-item {
    display: flex;
    flex-direction: column;

    .design-project-task-item-header {
        display: flex;
        flex-direction: row;
        align-items: center;
        gap: 0.5rem;

        .design-project-task-item-link {
            font-size: 12pt;
            font-weight: 600;

            &:hover {
                text-decoration: 2px underline var(--foreground);
            }
        }

        .design-project-task-item-status {
            background-color: var(--muted-foreground);

            &.todo {
                background-color: var(--todo);
            }

            &.progress {
                background-color: var(--in-progress);
            }

            &.completed {
                background-color: var(--completed);
            }
        }
    }

    .design-project-task-item-description {
        font-weight: 500;
        font-size: 10pt;
        color: var(--muted-foreground);
    }
}
</style>
