<script setup lang="ts">
import DropdownMenu from '../ui/dropdown-menu/DropdownMenu.vue';
import DropdownMenuTrigger from '../ui/dropdown-menu/DropdownMenuTrigger.vue';
import DropdownMenuContent from '../ui/dropdown-menu/DropdownMenuContent.vue';
import DropdownMenuItem from '../ui/dropdown-menu/DropdownMenuItem.vue';
import { useI18n } from 'vue-i18n';
import { getDesignTypes } from '@/persistence/dtos/design-project.dtos';
import Button from '../ui/button/Button.vue';
import Icon from '../Icon.vue';

const { t } = useI18n()
const designTypes = getDesignTypes()

const props = defineProps<{
    id?: string
    disabled?: boolean
    modelValue: number
}>()
const emits = defineEmits<{
    (e: "update:modelValue", type: number): void
}>()

function getIconForType(type: number) {
    switch (type) {
        case 0: return "bi bi-code-slash"
        case 1: return "bi bi-diagram-2"
        case 2: return "bi bi-diagram-2"
        default: return ""
    }
}
</script>

<template>
    <DropdownMenu>
        <DropdownMenuTrigger as-child class="design-type-picker">
            <Button :disabled variant="outline" :id>
                <Icon :icon="getIconForType(modelValue)" />

                {{ t(`design.types.type${modelValue}`) }}
            </Button>
        </DropdownMenuTrigger>

        <DropdownMenuContent class="design-type-picker-content">
            <DropdownMenuItem
                v-for="type in designTypes"
                :key="type"
                @click="emits('update:modelValue', type)"
            >
                <Icon :icon="getIconForType(type)" />

                {{ t(`design.types.type${type}`) }}
            </DropdownMenuItem>
        </DropdownMenuContent>
    </DropdownMenu>
</template>

<style>
.design-type-picker {
    width: 250px;
    justify-content: start;
}

.design-type-picker-content {
    width: 250px;
}
</style>
