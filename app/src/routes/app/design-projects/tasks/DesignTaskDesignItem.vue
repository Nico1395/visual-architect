<script setup lang="ts">
import { cn } from '@/lib/utils';

const props = defineProps<{
    to: string,
    name: string,
}>();
</script>

<template>
    <RouterLink :to :class="cn('data-active:focus:bg-accent data-active:hover:bg-accent data-active:bg-accent/50 data-active:text-accent-foreground hover:bg-accent hover:text-accent-foreground focus:bg-accent focus:text-accent-foreground ring-ring/10 dark:ring-ring/20 dark:outline-ring/40 outline-ring/50 [&_svg:not([class*=\'text-\'])]:text-muted-foreground flex flex-col gap-1 rounded-sm p-2 text-sm transition-[color,box-shadow] focus-visible:ring-4 focus-visible:outline-1 [&_svg:not([class*=\'size-\'])]:size-4 design-task-design-item')">
        <div class="design-task-design-item-preview">
            <Icon icon="ai-image" />
        </div>

        <div class="design-task-design-item-name">
            {{ props.name }}
        </div>
    </RouterLink>
</template>

<style>
.design-task-design-item {
    flex: none;
    border: 1px solid var(--border);
    border-radius: var(--radius-xl);
    padding: 1rem;
    box-sizing: border-box;
    width: 100%;
    aspect-ratio: 1;
    cursor: pointer;
    display: flex;
    flex-direction: column;
    gap: 1rem;

    .design-task-design-preview {
        flex: 1;
        display: flex;
        align-items: center;
        justify-content: center;
        border-radius: var(--radius-xl);

        > .ai-image {
            font-size: 50pt;
            opacity: 0.2;
        }
    }

    .design-task-design-name {
        text-align: center;
        font-size: 12pt;
        font-weight: 500;
        min-height: 2rem;
    }
}
</style>
