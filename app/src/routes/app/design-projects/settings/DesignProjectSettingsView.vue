<script setup lang="ts">
import { useI18n } from 'vue-i18n';
import DesignProjectSettingsViewField from './DesignProjectSettingsViewField.vue';
import { computed, inject, reactive, watch, type ComputedRef } from 'vue';
import { useDesignProjectStore } from '@/persistence/stores/design-project.store';
import type { DesignProjectDtoV1 } from '@/persistence/dtos/design-project.dtos';
import Input from '@/components/ui/input/Input.vue';
import { toast } from 'vue-sonner';

const { t } = useI18n();
const designProjectStore = useDesignProjectStore()
const project = inject<ComputedRef<DesignProjectDtoV1 | undefined>>('design-project')
if (!project) {
  throw new Error('DesignProject not provided')
}

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
    </div>
</template>

<style>
.design-project-settings {
    display: flex;
    flex-direction: column;
    gap: 2rem;
}
</style>
