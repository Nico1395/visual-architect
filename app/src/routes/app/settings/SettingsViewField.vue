<script setup lang="ts">
import Icon from '@/components/Icon.vue';
import ButtonGroup from '@/components/ui/button-group/ButtonGroup.vue';
import Button from '@/components/ui/button/Button.vue';

const props = defineProps<{
    class?: string | null,
    inputClass?: string | null,
    changed?: boolean,
    disabled?: boolean,
}>()

const emits = defineEmits<{
  (e: "save"): void,
  (e: "reset"): void,
}>()

function save() {
  emits("save")
}

function reset() {
    emits("reset")
}
</script>

<template>
    <div :class="`settings-view-field ${props.class}`.trim()">
        <h2>
            <slot name="name" />
        </h2>

        <ButtonGroup :class="`input ${props.inputClass}`.trim()">
            <slot name="input" />

            <Button variant="outline" v-if="props.changed" @click="reset()" :disabled>
                <Icon icon="ai-arrow-counter-clockwise" />
            </Button>

            <Button variant="successful" v-if="props.changed" @click="save()" :disabled>
                <Icon icon="ai-check" />
            </Button>
        </ButtonGroup>
    </div>
</template>

<style>
.settings-view-field {
    > h2 {
        font-weight: 700;
        font-size: 12pt;
        cursor: default;
        margin-bottom: 0.3rem;
    }

    > .input {
    }
}
</style>
