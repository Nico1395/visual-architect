<script setup lang="ts">
import { useColorMode } from "@vueuse/core"

useColorMode({
    attribute: "class",
    modes: {
        light: "light",
        dark: "dark",
    },
})
</script>

<template>
    <router-view />
</template>
