<script setup lang="ts">
import Icon from "@/components/Icon.vue"
import DropdownMenuItem from "@/components/ui/dropdown-menu/DropdownMenuItem.vue"

const props = defineProps<{
    title: string
    description: string
    icon: string
    iconSize?: string | null
}>()
const emit = defineEmits<{
    (e: 'click', event: PointerEvent): void
}>();

</script>

<template>
    <DropdownMenuItem class="user-menu-item">
        <button @click="emit('click', $event)">
            <div class="icon-section">
                <Icon :icon :size="props.iconSize" />
            </div>

            <div class="texts">
                <div class="title">{{ props.title }}</div>
                <div class="description">{{ props.description }}</div>
            </div>
        </button>
    </DropdownMenuItem>
</template>

<style>
.user-menu-item {
    padding: 0.7rem;

    button {
        display: flex;
        flex-direction: row;
        gap: 0.7rem;
        text-align: start;
        cursor: pointer;

        .icon-section {
            flex: 0;
        }

        .texts {
            flex: 1;

            .title {
                font-weight: 500;
                margin-bottom: 0.1rem;
            }

            .description {
                color: var(--muted-foreground);
            }
        }
    }
}
</style>
