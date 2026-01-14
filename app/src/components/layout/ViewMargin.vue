<script setup lang="ts">
const props = defineProps<{
    class?: string | null
}>()

function getClasses() {
    return `view-margin ${props.class}`.trim();
}
</script>

<template>
    <div :class="getClasses()">
        <slot />
    </div>
</template>

<style>
.view-margin {
    width: 70%;
    min-width: 1000px;
    margin: 1rem auto;
    box-sizing: border-box;
    padding: 0 1rem;
}

@media (max-width: calc(1000px)) {
    .view-margin {
        width: 100%;
        min-width: unset;
        margin: 0;
    }
}
</style>
