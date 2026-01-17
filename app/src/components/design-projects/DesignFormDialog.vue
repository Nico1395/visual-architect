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
import PreviewableMarkdownEditor from "@/components/PreviewableMarkdownEditor.vue";
import { useI18n } from "vue-i18n";
import { useDesignProjectStore } from "@/persistence/stores/design-project.store";
import { reactive, ref, watch } from 'vue';
import { toast } from 'vue-sonner';
import Button from '../ui/button/Button.vue';
import Icon from '../Icon.vue';
import DesignTypePicker from './DesignTypePicker.vue';

const props = defineProps<{
    opened: boolean,
    taskId: string
}>();
const emits = defineEmits<{
  (e: 'update:opened', value: boolean): void
  (e: 'submitted', payload: { designId: string | undefined }): void
}>();

const { t } = useI18n()
const designProjectStore = useDesignProjectStore();
const designDialogOpened = ref(false);
const form = reactive({
    name: "",
    descriptionPayload: "",
    type: 0,
})
function resetForm() {
    form.descriptionPayload = "";
    form.name = "";
    form.type = 0;
}

function close() {
    resetForm();
    designDialogOpened.value = false
    emits('update:opened', false);
}

async function saveDesign() {
    const promise = designProjectStore.addDesign(props.taskId, {
        name: form.name,
        descriptionPayload: form.descriptionPayload,
        type: form.type,
    })

    toast.promise(promise, {
        loading: t('toasts.saving.loading'),
        success: t('toasts.saving.success'),
        error: t('toasts.saving.error'),
    })

    const result = await promise

    emits('submitted', {
        designId: result?.designId
    });

    close()
}

watch(
    () => props.opened,
    (isOpen) => {
        designDialogOpened.value = isOpen;
        resetForm();
    },
    { immediate: true }
);
</script>

<template>
    <Dialog v-model:open="designDialogOpened">
        <DialogContent class="design-dialog">
            <form @submit.prevent="saveDesign">
                <DialogHeader>
                    <DialogTitle>
                        {{ t('designTask.designs.formDialog.title') }}
                    </DialogTitle>

                    <DialogDescription>
                        {{ t('designTask.designs.formDialog.description') }}
                    </DialogDescription>
                </DialogHeader>

                <div class="design-dialog-fields">
                    <div class="design-dialog-field">
                        <Label for="design-name">
                            {{ t('designTask.designs.formDialog.nameLabel') }}
                        </Label>

                        <Input id="design-name" v-model="form.name" :disabled="designProjectStore.busy" />
                    </div>

                    <div class="design-dialog-field">
                        <Label for="design-type">
                            {{ t('designTask.designs.formDialog.typeLabel') }}
                        </Label>

                        <DesignTypePicker id="design-type" v-model="form.type" :disabled="designProjectStore.busy" />
                    </div>

                    <div class="design-dialog-field">
                        <Label for="design-description">
                            {{ t('designTask.designs.formDialog.descriptionLabel') }}
                        </Label>

                        <PreviewableMarkdownEditor id="design-description" class="design-description-editor" v-model="form.descriptionPayload" :disabled="designProjectStore.busy" />
                    </div>
                </div>

                <DialogFooter class="flex justify-end gap-2">
                    <Button variant="outline" type="button" @click="close" :disabled="designProjectStore.busy">
                        <Icon icon="ai-cross" />

                        {{ t('actions.discard') }}
                    </Button>

                    <Button as-child variant="default" type="submit" :disabled="designProjectStore.busy">
                        <button type="submit">
                            <Icon icon="ai-check" />

                            {{ t('actions.create') }}
                        </button>
                    </Button>
                </DialogFooter>
            </form>
        </DialogContent>
    </Dialog>
</template>

<style>
.design-dialog {
    width: 95vw;
    max-width: 64rem;

    > form {
        display: flex;
        flex-direction: column;
        gap: 1rem;

        .design-dialog-fields {
            display: flex;
            flex-direction: column;
            gap: 1rem;

            .design-dialog-field {
                display: flex;
                flex-direction: column;
                gap: 0.5rem;
            }
        }
    }
}
</style>
