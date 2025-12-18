<script setup lang="ts">
import { computed } from "vue"
import { useRoute } from "vue-router"
import AppLayout from "./routes/app/AppLayout.vue"
import { useColorMode } from "@vueuse/core"

useColorMode({
    attribute: "class",
    modes: {
        light: "light",
        dark: "dark",
    },
})

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
