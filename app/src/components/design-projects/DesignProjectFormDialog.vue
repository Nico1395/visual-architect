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

const props = defineProps<{
    opened: boolean,
    form?: { name: string, description?: string | null }
}>();
const emits = defineEmits<{
  (e: 'update:opened', value: boolean): void
  (e: 'submitted', payload: { projectId: string | null | undefined }): void
}>();

const { t } = useI18n()
const designProjectStore = useDesignProjectStore();
const projectDialogOpened = ref(false);
const projectForm = reactive({
    name: "",
    description: "",
})

function resetForm() {
    projectForm.description = "";
    projectForm.name = "";
}

function closeProjectDialog() {
    resetForm();
    projectDialogOpened.value = false
    emits('update:opened', false);
}

async function saveProject() {
    const promise = designProjectStore.addProject(projectForm.name, projectForm.description)
    toast.promise(promise, {
        loading: t('toasts.saving.loading'),
        success: t('toasts.saving.success'),
        error: t('toasts.saving.error'),
    })

    const result = await promise

    emits('submitted', {
        projectId: result?.projectId,
    });

    closeProjectDialog()
}

watch(
    () => props.opened,
    (isOpen) => {
        projectDialogOpened.value = isOpen;
        resetForm();
    },
    { immediate: true }
);
</script>

<template>
    <Dialog v-model:open="projectDialogOpened">
        <DialogContent class="project-dialog">
            <form @submit.prevent="saveProject">
                <DialogHeader>
                    <DialogTitle>
                        {{ t('home.newprojdg.title') }}
                    </DialogTitle>

                    <DialogDescription>
                        {{ t('home.newprojdg.description') }}
                    </DialogDescription>
                </DialogHeader>

                <div class="project-dialog-fields">
                    <div class="project-dialog-field">
                        <Label for="project-name">
                            {{ t('home.newprojdg.namelabel') }}
                        </Label>

                        <Input id="project-name" v-model="projectForm.name" :disabled="designProjectStore.busy" />
                    </div>

                    <div class="project-dialog-field">
                        <Label for="project-description">
                            {{ t('home.newprojdg.descriptionlabel') }}
                        </Label>

                        <PreviewableMarkdownEditor id="project-description" class="project-description-editor" v-model="projectForm.description" :disabled="designProjectStore.busy" />
                    </div>
                </div>

                <DialogFooter class="flex justify-end gap-2">
                    <Button variant="outline" type="button" @click="closeProjectDialog" :disabled="designProjectStore.busy">
                        <Icon icon="ai-cross" />

                        {{ t('home.newprojdg.cancel') }}
                    </Button>

                    <Button as-child variant="default" type="submit" :disabled="designProjectStore.busy">
                        <button type="submit">
                            <Icon icon="ai-check" />

                            {{ t('home.newprojdg.create') }}
                        </button>
                    </Button>
                </DialogFooter>
            </form>
        </DialogContent>
    </Dialog>
</template>

<style>
.project-dialog {
    width: 95vw;
    max-width: 64rem;
    height: 80vw;
    max-height: 39rem;

    > form {
        display: flex;
        flex-direction: column;
        gap: 1rem;

        .project-dialog-fields {
            display: flex;
            flex-direction: column;
            gap: 1rem;

            .project-dialog-field {
                display: flex;
                flex-direction: column;
                gap: 0.5rem;
            }
        }
    }
}
</style>
