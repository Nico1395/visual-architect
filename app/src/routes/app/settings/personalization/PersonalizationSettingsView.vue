<script setup lang="ts">
import SettingsViewField from '../SettingsViewField.vue';
import SettingsViewHeader from '../SettingsViewHeader.vue';
import { useI18n } from "vue-i18n"
import { ref } from 'vue'
import { useColorMode } from '@vueuse/core';
import ThemePreview from './ThemePreview.vue';
import Toggle from '@/components/ui/toggle/Toggle.vue';
import Icon from '@/components/Icon.vue';
import { usePreferenceStore } from '@/persistence/stores/preference.store';
import LanguagePicker from './LanguagePicker.vue';

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
</script>

<template>
    <div class="personalization-settings">
        <div class="personalization-settings-header">
            <SettingsViewHeader>
                <template #settings-header>
                    {{ t('settings.personalization.header') }}
                </template>

                <template #settings-description>
                    {{ t('settings.personalization.description') }}
                </template>
            </SettingsViewHeader>
        </div>

        <SettingsViewField class="personalization-settings-theme-field">
            <template #name>
                {{ t('settings.personalization.theme.title') }}
            </template>

            <template #input>
                <Toggle
                    style="width: fit-content"
                    variant="outline"
                    v-model:model-value="useSystemTheme"
                    v-on:update:model-value="(useSystem) => setSystemAsTheme(useSystem)">
                    {{ t('settings.personalization.theme.usesystem') }}

                    <Icon icon="ai-desktop-device" />
                </Toggle>

                <div class="personalization-settings-theme-picker">
                    <ThemePreview theme="light" @click="setTheme((mode = 'light'))" :disabled="useSystemTheme">
                        {{ t('settings.personalization.theme.light') }}
                    </ThemePreview>

                    <ThemePreview theme="dark" @click="setTheme((mode = 'dark'))" :disabled="useSystemTheme">
                        {{ t('settings.personalization.theme.dark') }}
                    </ThemePreview>
                </div>
            </template>

            <template #desc>
                {{ t('settings.personalization.theme.description') }}
            </template>
        </SettingsViewField>

        <SettingsViewField>
            <template #name>
                {{ t('settings.personalization.language.title') }}
            </template>

            <template #input>
                <LanguagePicker
                    v-model="currentLanguage"
                    @update:modelValue="setLanguage"
                />
            </template>
        </SettingsViewField>
    </div>
</template>

<style>
.personalization-settings {
    display: flex;
    flex-direction: column;
    gap: 2rem;

    .personalization-settings-theme-field {
        > div {
            width: 100%;
            display: flex;
            flex-direction: column;
            gap: 1rem;

            .personalization-settings-use-system-theme {
                font-size: 11pt;
                cursor: pointer;
                width: fit-content;
            }

            .personalization-settings-theme-picker {
                width: 100%;
                display: grid;
                grid-template-columns: repeat(auto-fill, 300px);
                gap: 0.5rem;
            }
        }
    }

}
</style>
