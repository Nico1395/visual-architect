<script setup lang="ts">
import { computed } from "vue"
import { useRoute } from "vue-router"
import AppLayout from "./routes/app/AppLayout.vue"

const route = useRoute()
const layout = computed(() => {
    return route.meta.layout || AppLayout
})
</script>

<template>
    <component :is="layout">
        <router-view />
    </component>
</template>
