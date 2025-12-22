<script setup lang="ts">
import { useColorMode } from "@vueuse/core"
import { Toaster } from 'vue-sonner';

useColorMode({
    attribute: "class",
    modes: {
        light: "light",
        dark: "dark",
    },
})
</script>

<template>
    <RouterView />

    <Toaster position="top-center" richColors />
</template>
