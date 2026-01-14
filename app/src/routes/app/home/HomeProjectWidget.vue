<script setup lang="ts">
import Icon from '@/components/Icon.vue';
import { cn } from '@/lib/utils';
import type { DesignProjectDtoV1 } from '@/persistence/dtos/design-project.dtos';

const props = defineProps<{
    project: DesignProjectDtoV1
}>();
</script>

<template>
    <RouterLink :to="`/app/design-projects/${props.project.id}`" :class="cn('data-active:focus:bg-accent data-active:hover:bg-accent data-active:bg-accent/50 data-active:text-accent-foreground hover:bg-accent hover:text-accent-foreground focus:bg-accent focus:text-accent-foreground ring-ring/10 dark:ring-ring/20 dark:outline-ring/40 outline-ring/50 [&_svg:not([class*=\'text-\'])]:text-muted-foreground flex flex-col gap-1 rounded-sm p-2 text-sm transition-[color,box-shadow] focus-visible:ring-4 focus-visible:outline-1 [&_svg:not([class*=\'size-\'])]:size-4 project-widget')">
        <div class="project-widget-preview">
            <Icon icon="ai-image" />
        </div>

        <div class="project-widget-name">
            {{ props.project.name }}
        </div>
    </RouterLink>
</template>

<style>
.project-widget {
    flex: none;
    border: 1px solid var(--border);
    border-radius: var(--radius-xl);
    padding: 1rem;
    box-sizing: border-box;
    width: 100%;
    height: 250px;
    cursor: pointer;
    display: flex;
    flex-direction: column;
    gap: 1rem;

    .project-widget-preview {
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

    .project-widget-name {
        text-align: center;
        font-size: 12pt;
        font-weight: 500;
        min-height: 2rem;
    }
}
</style>
