<script setup lang="ts">
import SettingsViewField from '../PreferencesViewField.vue';
import SettingsViewHeader from '../PreferencesViewHeader.vue';
import { useI18n } from "vue-i18n"
import { ref } from 'vue'
import { useColorMode } from '@vueuse/core';
import ThemePreview from './ThemePreview.vue';
import Toggle from '@/components/ui/toggle/Toggle.vue';
import Icon from '@/components/Icon.vue';
import { usePreferenceStore } from '@/persistence/stores/preference.store';
import LanguagePicker from './LanguagePicker.vue';
import { toast } from 'vue-sonner'

const mode = useColorMode()
const { t, locale } = useI18n();
const preferenceStore = usePreferenceStore()
const useSystemTheme = ref(false)
const currentLanguage = ref(preferenceStore.getCachedPreference('language') || 'en')

async function setSystemAsTheme(useSystem: boolean | undefined) {
    useSystemTheme.value = useSystem ?? useSystemTheme.value

    if (useSystemTheme.value)
        mode.store.value = 'auto'

    await setTheme("auto")
}

async function setTheme(theme: string) {
    await preferenceStore.setPreference("theme", theme)
}

async function setLanguage(language: string) {
    locale.value = language
    await preferenceStore.setPreference("language", language)
}

async function saveWithToast(promise: Promise<void>) {
    toast.promise(promise, {
        loading: t('toasts.saving.loading'),
        success: t('toasts.saving.success'),
        error: t('toasts.saving.error'),
    });

    await promise;
}
</script>

<template>
    <div class="personalization-preferences">
        <div class="personalization-preferences-header">
            <SettingsViewHeader>
                <template #preferences-header>
                    {{ t('preferences.personalization.header') }}
                </template>

                <template #preferences-description>
                    {{ t('preferences.personalization.description') }}
                </template>
            </SettingsViewHeader>
        </div>

        <SettingsViewField class="personalization-preferences-theme-field">
            <template #name>
                {{ t('preferences.personalization.theme.title') }}
            </template>

            <template #input>
                <Toggle
                    style="width: fit-content"
                    variant="outline"
                    v-model:model-value="useSystemTheme"
                    v-on:update:model-value="(useSystem) => saveWithToast(setSystemAsTheme(useSystem))">
                    {{ t('preferences.personalization.theme.usesystem') }}

                    <Icon icon="ai-desktop-device" />
                </Toggle>

                <div class="personalization-preferences-theme-picker">
                    <ThemePreview theme="light" @click="saveWithToast(setTheme((mode = 'light')))" :disabled="useSystemTheme">
                        {{ t('preferences.personalization.theme.light') }}
                    </ThemePreview>

                    <ThemePreview theme="dark" @click="saveWithToast(setTheme((mode = 'dark')))" :disabled="useSystemTheme">
                        {{ t('preferences.personalization.theme.dark') }}
                    </ThemePreview>
                </div>
            </template>

            <template #desc>
                {{ t('preferences.personalization.theme.description') }}
            </template>
        </SettingsViewField>

        <SettingsViewField>
            <template #name>
                {{ t('preferences.personalization.language.title') }}
            </template>

            <template #input>
                <LanguagePicker
                    v-model="currentLanguage"
                    @update:modelValue="(language) => saveWithToast(setLanguage(language))"
                />
            </template>
        </SettingsViewField>
    </div>
</template>

<style>
.personalization-preferences {
    display: flex;
    flex-direction: column;
    gap: 2rem;

    .personalization-preferences-theme-field {
        > div {
            width: 100%;
            display: flex;
            flex-direction: column;
            gap: 1rem;

            .personalization-preferences-use-system-theme {
                font-size: 11pt;
                cursor: pointer;
                width: fit-content;
            }

            .personalization-preferences-theme-picker {
                width: 100%;
                display: grid;
                grid-template-columns: repeat(auto-fill, 300px);
                gap: 0.5rem;
            }
        }
    }

}
</style>
