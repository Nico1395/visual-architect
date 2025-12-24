<script setup lang="ts">
import Icon from '@/components/Icon.vue'
import Button from '@/components/ui/button/Button.vue'
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
import { computed, ref } from 'vue'
import { useI18n } from 'vue-i18n'

const { t } = useI18n()
const languages = [
  { value: 'en', label: 'English' },
  { value: 'de', label: 'Deutsch' },
]
const props = defineProps<{
  modelValue: string
}>()
const emit = defineEmits<{
  (e: 'update:modelValue', value: string): void
}>()
const open = ref(false)
const currentValue = computed({
  get() {
    return (
      languages.find(l => l.value === props.modelValue)?.value ?? languages[0]!.value
    )
  },
  set(value: string) {
    emit('update:modelValue', value)
  },
})
const selectedLanguage = computed(() =>
  languages.find(l => l.value === currentValue.value),
)

function selectLanguage(lang: string) {
  if (lang !== currentValue.value) {
    currentValue.value = lang
  }
  open.value = false
}
</script>

<template>
  <Popover v-model:open="open">
    <PopoverTrigger as-child>
      <Button
        variant="outline"
        role="combobox"
        :aria-expanded="open"
        class="w-[200px] justify-between"
      >
        {{ selectedLanguage?.label }}

        <Icon icon="ai-chevron-vertical" />
      </Button>
    </PopoverTrigger>

    <PopoverContent class="w-[200px] p-0">
      <Command>
        <CommandInput
          class="h-9"
          :placeholder="t('preferences.personalization.language.search')"
        />
        <CommandList>
          <CommandEmpty>
            {{ t('preferences.personalization.language.notfound') }}
          </CommandEmpty>

          <CommandGroup>
            <CommandItem
              v-for="language in languages"
              :key="language.value"
              :value="language.value"
              @select="() => selectLanguage(language.value)"
            >
              {{ language.label }}

              <Icon
                v-if="currentValue === language.value"
                icon="ai-check"
              />
            </CommandItem>
          </CommandGroup>
        </CommandList>
      </Command>
    </PopoverContent>
  </Popover>
</template>

