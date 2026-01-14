<script setup lang="ts">
import { useI18n } from 'vue-i18n';
import DesignProjectSettingsViewField from './DesignProjectSettingsViewField.vue';
import { computed, inject, reactive, ref, watch, type ComputedRef } from 'vue';
import { useDesignProjectStore } from '@/persistence/stores/design-project.store';
import type { DesignProjectDtoV1 } from '@/persistence/dtos/design-project.dtos';
import Input from '@/components/ui/input/Input.vue';
import { toast } from 'vue-sonner';
import {
  Item,
  ItemActions,
  ItemContent,
  ItemDescription,
  ItemTitle,
} from '@/components/ui/item'
import {
  Dialog,
  DialogContent,
  DialogHeader,
  DialogTitle,
  DialogDescription,
  DialogFooter
} from '@/components/ui/dialog';
import Button from '@/components/ui/button/Button.vue';
import { useRouter } from 'vue-router';

const { t } = useI18n();
const designProjectStore = useDesignProjectStore()
const project = inject<ComputedRef<DesignProjectDtoV1 | undefined>>('design-project')
if (!project) {
  throw new Error('DesignProject not provided')
}
const router = useRouter()

const form = reactive({
    id: "",
    name: "",
    descriptionPayload: "",
})
const originalForm = reactive({
    id: "",
    name: "",
    descriptionPayload: "",
})
const nameChanged = computed(() => form.name !== originalForm.name)
const showDeleteDialog = ref(false);

function resetField<K extends keyof typeof form>(key: K) {
    form[key] = originalForm[key]
}

async function updateName() {
    await saveChanges(designProjectStore.updateProject({ id: form.id, name: form.name }))
    originalForm.name = form.name
}

async function saveChanges(promise: Promise<void>) {
    toast.promise(promise, {
        loading: t('toasts.saving.loading'),
        success: t('toasts.saving.success'),
        error: t('toasts.saving.error'),
    });

    await promise;
}

async function confirmDelete() {
    if (!project?.value)
        return

    const promise = designProjectStore.deleteProject(project?.value.id)
    toast.promise(promise, {
        loading: t('designprojects.settings.delete.toast.loading'),
        success: t('designprojects.settings.delete.toast.success'),
        error: t('designprojects.settings.delete.toast.error'),
    });

    await promise;
    router.push({
        path: `/app/home`
    })
}

watch(project, (p) => {
    if (!p)
        return

    Object.assign(originalForm, {
        id: project.value?.id,
        name: project.value?.name,
        descriptionPayload: project.value?.descriptionPayload
    })

    Object.assign(form, {
        id: project.value?.id,
        name: project.value?.name,
        descriptionPayload: project.value?.descriptionPayload
    })
  },
  { immediate: true }
)
</script>

<template>
    <div class="design-project-settings">
        <DesignProjectSettingsViewField
            :disabled="designProjectStore.busy"
            :changed="nameChanged"
            @reset="resetField('name')"
            @save="updateName()"
            :grouped="true"
        >
            <template #name>
                {{ t('designprojects.settings.name.label') }}
            </template>

            <template #input>
                <Input
                    type="text"
                    length="256"
                    v-model="form.name"
                    :disabled="designProjectStore.busy"
                />
            </template>

            <template #desc>
                    {{ t('designprojects.settings.name.description') }}
            </template>
        </DesignProjectSettingsViewField>

        <Item variant="outline" class="design-project-delete-card">
            <ItemContent class="design-project-delete-card-content">
                <ItemTitle class="design-project-delete-title">
                    {{ t('designprojects.settings.delete.title') }}
                </ItemTitle>

                <ItemDescription class="design-project-delete-description">
                    {{ t('designprojects.settings.delete.description') }}
                </ItemDescription>
            </ItemContent>

            <ItemActions>
                <Button variant="destructive" @click="showDeleteDialog = true">
                    {{ t('designprojects.settings.delete.callToAction') }}
                </Button>
            </ItemActions>
        </Item>
    </div>

    <Dialog v-model:open="showDeleteDialog">
        <DialogContent>
            <DialogHeader>
                <DialogTitle>
                    {{ t('designprojects.settings.delete.modal.title') }}
                </DialogTitle>

                <DialogDescription>
                    {{ t('designprojects.settings.delete.modal.description') }}
                </DialogDescription>
            </DialogHeader>
            <DialogFooter class="flex justify-end gap-2">
                <Button variant="outline" @click="showDeleteDialog = false" :disabled="designProjectStore.busy">
                    {{ t('designprojects.settings.delete.modal.cancel') }}
                </Button>

                <Button variant="destructive" @click="confirmDelete" :disabled="designProjectStore.busy">
                    {{ t('designprojects.settings.delete.modal.confirm') }}
                </Button>
            </DialogFooter>
        </DialogContent>
    </Dialog>
</template>

<style>
.design-project-settings {
    display: flex;
    flex-direction: column;
    gap: 2rem;

    .design-project-delete-card {
        max-width: 500px;
        border-color: rgb(from var(--destructive) r g b / 0.2);
        color: var(--destructive);
        background-color: rgb(from var(--destructive) r g b / 0.05);
        flex-direction: column;
        align-items: start;

        .design-project-delete-card-content {
            gap: calc(var(--spacing) * 4);

            .design-project-delete-title {
                font-size: 12pt;
                font-weight: 700;
            }

            .design-project-delete-description {
                color: inherit;
                overflow: visible;
                display: block;
                -webkit-line-clamp: unset;
                -webkit-box-orient: unset;
            }
        }
    }
}
</style>
