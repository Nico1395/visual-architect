<script setup lang="ts">
import {
  Dialog,
  DialogContent,
  DialogHeader,
  DialogTitle,
  DialogDescription,
  DialogFooter
} from '@/components/ui/dialog';
import Input from "@/components/ui/input/Input.vue";
import Label from "@/components/ui/label/Label.vue";
import MarkdownEditor from "@/components/MarkdownEditor.vue";
import { useI18n } from "vue-i18n";
import { useDesignProjectStore } from "@/persistence/stores/design-project.store";
import { reactive, ref, watch } from 'vue';
import { toast } from 'vue-sonner';
import Button from '../ui/button/Button.vue';
import Icon from '../Icon.vue';

const props = defineProps<{
    opened: boolean,
    form?: { name: string, description?: string | null }
}>();
const emits = defineEmits<{
  (e: 'update:opened', value: boolean): void
  (e: 'submitted', payload: { taskNumber: number | null | undefined }): void
}>();

const { t } = useI18n()
const designProjectStore = useDesignProjectStore();
const taskDialogOpened = ref(false);
const taskForm = reactive({
    name: "",
    description: "",
})

function resetForm() {
    taskForm.description = "";
    taskForm.name = "";
}

function closeTaskDialog() {
    resetForm();
    taskDialogOpened.value = false
    emits('update:opened', false);
}

async function saveTask() {
    const promise = designProjectStore.addTask(taskForm.name, taskForm.description)
    toast.promise(promise, {
        loading: t('toasts.saving.loading'),
        success: t('toasts.saving.success'),
        error: t('toasts.saving.error'),
    })

    const result = await promise

    emits('submitted', {
        taskNumber: result
    });

    closeTaskDialog()
}

watch(
    () => props.opened,
    (isOpen) => {
        taskDialogOpened.value = isOpen;
        resetForm();
    },
    { immediate: true }
);
</script>

<template>
    <Dialog v-model:open="taskDialogOpened">
        <DialogContent class="task-dialog">
            <form @submit.prevent="saveTask">
                <DialogHeader>
                    <DialogTitle>
                        {{ t('designprojects.tasks.formDialog.title') }}
                    </DialogTitle>

                    <DialogDescription>
                        {{ t('designprojects.tasks.formDialog.description') }}
                    </DialogDescription>
                </DialogHeader>

                <div class="task-dialog-fields">
                    <div class="task-dialog-field">
                        <Label for="task-name">
                            {{ t('designprojects.tasks.formDialog.nameLabel') }}
                        </Label>

                        <Input id="task-name" v-model="taskForm.name" :disabled="designProjectStore.busy" />
                    </div>

                    <div class="task-dialog-field">
                        <Label for="task-description">
                            {{ t('designprojects.tasks.formDialog.descriptionLabel') }}
                        </Label>

                        <MarkdownEditor id="task-description" class="task-description-editor" v-model="taskForm.description" :disabled="designProjectStore.busy" />
                    </div>
                </div>

                <DialogFooter class="flex justify-end gap-2">
                    <Button variant="outline" type="button" @click="closeTaskDialog" :disabled="designProjectStore.busy">
                        <Icon icon="ai-cross" />

                        {{ t('designprojects.tasks.formDialog.discard') }}
                    </Button>

                    <Button as-child variant="default" type="submit" :disabled="designProjectStore.busy">
                        <button type="submit">
                            <Icon icon="ai-check" />

                            {{ t('designprojects.tasks.formDialog.create') }}
                        </button>
                    </Button>
                </DialogFooter>
            </form>
        </DialogContent>
    </Dialog>
</template>

<style>
.task-dialog {
    width: 95vw;
    max-width: 64rem;
    height: 80vw;
    max-height: 39rem;

    > form {
        display: flex;
        flex-direction: column;
        gap: 1rem;

        .task-dialog-fields {
            display: flex;
            flex-direction: column;
            gap: 1rem;

            .task-dialog-field {
                display: flex;
                flex-direction: column;
                gap: 0.5rem;
            }
        }
    }
}
</style>
