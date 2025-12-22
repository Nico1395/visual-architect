<script setup lang="ts">
const props = defineProps<{
    icon: string
    size?: string | null
}>()
</script>

<template>
    <i :class="`icon ${props.icon}`" :style="`font-size: ${props.size}`"></i>
</template>
