<script setup lang="ts">
import Icon from '@/components/Icon.vue';
import { useSlots } from 'vue'
import ButtonGroup from '@/components/ui/button-group/ButtonGroup.vue';
import Button from '@/components/ui/button/Button.vue';

const slots = useSlots()
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
    <div :class="`settings-view-field ${props.class ?? ''}`.trim()">
        <h2>
            <slot name="name" />
        </h2>

        <ButtonGroup :class="props.inputClass">
            <slot name="input" />

            <Button variant="outline" v-if="props.changed" @click="reset()" :disabled>
                <Icon icon="ai-arrow-counter-clockwise" />
            </Button>

            <Button variant="successful" v-if="props.changed" @click="save()" :disabled>
                <Icon icon="ai-check" />
            </Button>
        </ButtonGroup>

        <p v-if="slots.desc">
            <slot name="desc" />
        </p>
    </div>
</template>

<style>
.settings-view-field {
    display: flex;
    flex-direction: column;
    gap: 0.3rem;

    > h2 {
        font-weight: 700;
        font-size: 12pt;
        cursor: default;
    }

    > p {
        font-size: 10pt;
        font-weight: 500;
        color: var(--muted-foreground);
    }
}
</style>
