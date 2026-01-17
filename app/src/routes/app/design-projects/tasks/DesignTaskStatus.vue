<script setup lang="ts">
import Badge from '@/components/ui/badge/Badge.vue';
import {
    DropdownMenu, DropdownMenuContent, DropdownMenuItem, DropdownMenuTrigger
} from '@/components/ui/dropdown-menu'
import type { DesignTaskDtoV1 } from '@/persistence/dtos/design-project.dtos';
import { useI18n } from 'vue-i18n';

const { t } = useI18n();
const props = defineProps<{
    task: DesignTaskDtoV1
}>()
const emits = defineEmits<{
    (e: "status:selected", status: number): void
}>()

const statuses = [0, 1, 2];

function getStatusClass() {
    return getStatusClassForValue(props.task.status)
}

function getStatusClassForValue(value: number) {
    switch (value) {
        case 0: return "todo"
        case 1: return "progress"
        case 2: return "completed"
        default: return ""
    }
}

function selectStatus(status: number) {
    emits("status:selected", status)
}
</script>

<template>
    <DropdownMenu>
        <DropdownMenuTrigger as-child>
            <Badge :class="`design-task-status-badge ${getStatusClass()}`">
                {{ t(`designTask.status.${getStatusClass()}`) }}
            </Badge>
        </DropdownMenuTrigger>

        <DropdownMenuContent class="design-task-status-selector">
            <div class="design-task-status-selector-prompt">
                {{ t("designTask.status.setStatus") }}
            </div>

            <DropdownMenuItem
                v-for="status in statuses.filter(s => s != task.status)"
                :key="status"
                @click="selectStatus(status)"
            >
                <div :class="`design-task-status-blip ${getStatusClassForValue(status)}`"></div>

                {{ t(`designTask.status.${getStatusClassForValue(status)}`) }}
            </DropdownMenuItem>
        </DropdownMenuContent>
    </DropdownMenu>
</template>

<style>
.design-task-status-badge, .design-task-status-blip {
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

.design-task-status-badge {
    background-color: var(--muted-foreground);
    cursor: pointer;
}

.design-task-status-selector {
    padding: 0.5rem;

    .design-task-status-selector-prompt {
        font-size: 10pt;
        font-weight: 500;
        text-align: start;
        margin-bottom: 0.2rem;
        color: var(--muted-foreground);
    }
}


.design-task-status-blip {
    height: 12px;
    aspect-ratio: 1;
    border-radius: 7px;
}
</style>
