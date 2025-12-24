<script setup lang="ts">
import SettingsViewField from '../SettingsViewField.vue';
import SettingsViewHeader from '../SettingsViewHeader.vue';
import { useI18n } from "vue-i18n"
import { CheckIcon, ChevronsUpDownIcon } from 'lucide-vue-next'
import { computed, ref } from 'vue'
import { cn } from '@/lib/utils'
import { Button } from '@/components/ui/button'
import {
  Command,
  CommandEmpty,
  CommandGroup,
  CommandInput,
  CommandItem,
  CommandList,
} from '@/components/ui/command'
import {
  Popover,
  PopoverContent,
  PopoverTrigger,
} from '@/components/ui/popover'
import { useColorMode } from '@vueuse/core';
import ThemePreview from './ThemePreview.vue';
import Toggle from '@/components/ui/toggle/Toggle.vue';
import Icon from '@/components/Icon.vue';
import { usePreferenceStore } from '@/persistence/stores/preference.store';

const frameworks = [
    {
        value: 'next.js',
        label: 'Next.js',
    },
    {
        value: 'sveltekit',
        label: 'SvelteKit',
    },
    {
        value: 'nuxt.js',
        label: 'Nuxt.js',
    },
    {
        value: 'remix',
        label: 'Remix',
    },
    {
        value: 'astro',
        label: 'Astro',
    },
]

const mode = useColorMode()
const { t } = useI18n();
const preferenceStore = usePreferenceStore()

const open = ref(false)
const value = ref('')
const selectedFramework = computed(() =>
    frameworks.find(framework => framework.value === value.value),
)
const useSystemTheme = ref(false)

function selectFramework(selectedValue: string) {
    value.value = selectedValue === value.value ? '' : selectedValue
    open.value = false
}

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
                <Popover v-model:open="open">
                    <PopoverTrigger as-child>
                    <Button
                        variant="outline"
                        role="combobox"
                        :aria-expanded="open"
                        class="w-[200px] justify-between"
                    >
                        {{ selectedFramework?.label || "Select framework..." }}
                        <ChevronsUpDownIcon class="opacity-50" />
                    </Button>
                    </PopoverTrigger>
                    <PopoverContent class="w-[200px] p-0">
                    <Command>
                        <CommandInput class="h-9" placeholder="Search framework..." />
                        <CommandList>
                        <CommandEmpty>No framework found.</CommandEmpty>
                        <CommandGroup>
                            <CommandItem
                            v-for="framework in frameworks"
                            :key="framework.value"
                            :value="framework.value"
                            @select="(ev) => {
                                selectFramework(ev.detail.value as string)
                            }"
                            >
                            {{ framework.label }}
                            <CheckIcon
                                :class="cn(
                                'ml-auto',
                                value === framework.value ? 'opacity-100' : 'opacity-0',
                                )"
                            />
                            </CommandItem>
                        </CommandGroup>
                        </CommandList>
                    </Command>
                    </PopoverContent>
                </Popover>
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
