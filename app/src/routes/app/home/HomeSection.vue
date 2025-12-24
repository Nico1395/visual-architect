<script setup lang="ts">
    const props = defineProps<{
        class?: string | null,
        contentClass?: string | null
    }>()
</script>

<template>
    <section :class="`home-section ${props.class}`.trim()">
        <div class="home-section-header">
            <h1>
                <slot name="title" />
            </h1>

            <div class="actions">
                <slot name="actions" />
            </div>
        </div>

        <div class="home-section-description">
            <slot name="description" />
        </div>

        <div :class="`home-section-content ${props.contentClass}`.trim()">
            <slot name="content" />
        </div>
    </section>
</template>

<style>
.home-section {
    display: flex;
    flex-direction: column;
    gap: 1rem;

    .home-section-header {
        display: flex;
        flex-direction: row;
        justify-content: space-between;
        align-items: center;
        flex: none;

        > h1 {
            font-weight: 700;
            font-size: 26pt;
        }

        .actions {

        }
    }

    .home-section-description {
        flex: none;
        font-size: 11pt;
        font-weight: 500;
        color: var(--muted-foreground);
    }

    .home-section-content {
        flex: none;
    }
}
</style>
