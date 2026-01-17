<script setup lang="ts">
import Button from '@/components/ui/button/Button.vue';
import ButtonGroup from '@/components/ui/button-group/ButtonGroup.vue';
import {
    DropdownMenu,
    DropdownMenuContent,
    DropdownMenuItem,
    DropdownMenuTrigger
} from '@/components/ui/dropdown-menu'
import {
  Dialog,
  DialogContent,
  DialogHeader,
  DialogTitle,
  DialogDescription,
  DialogFooter
} from '@/components/ui/dialog';
import { useI18n } from 'vue-i18n';
import Icon from '@/components/Icon.vue';
import { ref } from 'vue';
import Popover from '@/components/ui/popover/Popover.vue';
import PopoverTrigger from '@/components/ui/popover/PopoverTrigger.vue';
import PopoverContent from '@/components/ui/popover/PopoverContent.vue';
import type { DesignTaskDtoV1 } from '@/persistence/dtos/design-project.dtos';

const { t, d } = useI18n();
const props = defineProps<{
    busy: boolean
    task: DesignTaskDtoV1
}>()
const emits = defineEmits<{
    (e: "deleted"): void
}>()

const showDeleteDialog = ref(false);

function confirmDelete() {
    emits("deleted")
}
</script>

<template>
    <ButtonGroup>
        <Popover>
            <PopoverTrigger as-child>
                <Button variant="outline" size="sm">
                    <Icon icon="ai-clock" />

                    {{ t('designTask.menu.history.item') }}
                </Button>
            </PopoverTrigger>

            <PopoverContent class="design-task-history" variant="destructive">
                <div>
                    {{ t('designTask.menu.history.createdAt', { timestamp: d(task.createdAt) }) }}
                </div>

                <div>
                    {{ t('designTask.menu.history.updatedAt', { timestamp: d(task.updatedAt) }) }}
                </div>
            </PopoverContent>
        </Popover>

        <DropdownMenu>
            <DropdownMenuTrigger as-child>
                <Button variant="outline" size="sm">
                    <Icon icon="ai-more-horizontal-fill" />
                </Button>
            </DropdownMenuTrigger>

            <DropdownMenuContent align="end" variant="destructive">
                <DropdownMenuItem variant="destructive" @click="showDeleteDialog = true">
                    <Icon icon="ai-trash-can" />

                    {{ t('designTask.menu.deleteTask.item') }}
                </DropdownMenuItem>
            </DropdownMenuContent>
        </DropdownMenu>
    </ButtonGroup>

    <Dialog v-model:open="showDeleteDialog">
        <DialogContent>
            <DialogHeader>
                <DialogTitle>
                    {{ t('designTask.menu.deleteTask.modal.title') }}
                </DialogTitle>

                <DialogDescription>
                    {{ t('designTask.menu.deleteTask.modal.description') }}
                </DialogDescription>
            </DialogHeader>
            <DialogFooter class="flex justify-end gap-2">
                <Button variant="outline" @click="showDeleteDialog = false" :disabled="props.busy">
                    {{ t('actions.cancel') }}
                </Button>

                <Button variant="destructive" @click="confirmDelete" :disabled="props.busy">
                    {{ t('actions.confirm') }}
                </Button>
            </DialogFooter>
        </DialogContent>
    </Dialog>
</template>

<style>
.design-task-history {
    width: fit-content;
    font-size: 10pt;
    font-weight: 500;
    color: var(--muted-foreground);
    display: flex;
    flex-direction: column;
    gap: 0.5rem;
}
</style>
