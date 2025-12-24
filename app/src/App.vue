<script setup lang="ts">
import { useColorMode } from "@vueuse/core"
import { Toaster } from 'vue-sonner';
import { mapToColorMode, useInitializationStore } from "./persistence/stores/initialization.store";
import LoaderView from "./routes/LoaderView.vue";
import { watchEffect } from "vue";
import { usePreferenceStore } from "./persistence/stores/preference.store";
import { useI18n } from "vue-i18n";

useColorMode({
    attribute: "class",
    modes: {
        light: "light",
        dark: "dark",
    },
})

const initializationStore = useInitializationStore()
const preferenceStore = usePreferenceStore()
const colorMode = useColorMode({
    initialValue: "light",
})
const { locale } = useI18n()

watchEffect(() => {
    if (!initializationStore.initialized)
        return

    const theme = preferenceStore.getCachedPreference("theme")
    colorMode.store.value = mapToColorMode(theme)

    const language = preferenceStore.getCachedPreference("language")
    locale.value = language ?? "en"
})

function getToasterTheme(): 'light' | 'dark' | 'system' {
    const theme = colorMode.store.value
    if (theme === "light") {
        return theme
    }

    if (theme === "auto") {
        return "system"
    }

    return "dark"
}
</script>

<template>
    <RouterView v-if="initializationStore.initialized" />
    <LoaderView v-else />

    <Toaster
        position="top-center"
        richColors
        closeButton
        :theme="getToasterTheme()" />
</template>
