<script setup lang="ts">
const props = defineProps<{
    to: string
    class?: string
}>()
</script>

<template>
    <RouterLink :class="`footer-item ${props.class ?? ''}`.trimEnd()" :to="props.to">
        <slot />
    </RouterLink>
</template>

<style>
.footer-item {
    flex: none;
    display: flex;
    flex-direction: row;
    gap: 0.5rem;

    &:hover {
        text-decoration: 1px underline;
    }
}
</style>
