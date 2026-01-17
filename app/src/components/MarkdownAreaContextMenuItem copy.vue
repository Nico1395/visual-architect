<script setup lang="ts">
import Icon from './Icon.vue';
import ContextMenuItem from './ui/context-menu/ContextMenuItem.vue';
import ContextMenuShortcut from './ui/context-menu/ContextMenuShortcut.vue';

const props = defineProps<{
    icon: string
}>()
const emits = defineEmits<{
    (e: "click"): void
}>()

function click() {
    emits("click")
}
</script>

<template>
    <ContextMenuItem class="markdown-input-context-menu-item" @click="click">
        <div class="markdown-input-context-menu-item-title">
            <Icon :icon />

            <slot name="title" />
        </div>

        <ContextMenuShortcut>
            <slot name="shortcut" />
        </ContextMenuShortcut>
    </ContextMenuItem>
</template>

<style>
.markdown-input-context-menu-item {
    display: flex;
    flex-direction: row;
    gap: 1rem;
    align-items: center;
    justify-content: space-between;

    .markdown-input-context-menu-item-title {
        display: flex;
        flex-direction: row;
        gap: 0.5rem;
        align-items: center;
    }
}
</style>
