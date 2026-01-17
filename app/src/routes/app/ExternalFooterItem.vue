<script setup lang="ts">
const props = defineProps<{
    to: string
    class?: string
}>()
</script>

<template>
    <a :class="`footer-item ${props.class ?? ''}`.trimEnd()" :href="props.to" target="_blank">
        <slot />
    </a>
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
