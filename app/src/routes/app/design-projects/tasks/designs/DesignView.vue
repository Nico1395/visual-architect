<script setup lang="ts">
import ViewMargin from '@/components/layout/ViewMargin.vue';
import { useDesignProjectStore } from '@/persistence/stores/design-project.store';
import { computed, watch } from 'vue';
import { useI18n } from 'vue-i18n';
import { useRoute } from 'vue-router';

const { t } = useI18n();
const route = useRoute()
const designProjectStore = useDesignProjectStore()

const projectId = computed(() => route.params.projectId as string)
const project = computed(() => designProjectStore.projects.find(p => p.id === projectId.value))
const taskNumber = computed(() => route.params.taskNumber as string)
const task = computed(() => project.value?.designTasks?.find(t => t.number.toString() === taskNumber.value))
const designId = computed(() => route.params.designId as string)
const design = computed(() => project.value?.designTasks?.find(t => t.number.toString() === taskNumber.value)?.designs?.find(d => d.id == designId.value))

// watch(
//     () => projectId.value,
//     (id) => designProjectStore.getProjectById(id),
//     { immediate: true }
// )

watch([projectId, taskNumber, designId], async ([pid, tnum, did]) => {
    if (!pid || !tnum || !did)
        return

    if (!design.value)
        await designProjectStore.getDesignByProjectIdTaskNumberAndId(projectId.value, Number(taskNumber.value), designId.value)
    },
    { immediate: true }
)
</script>

<template>
    <ViewMargin class="design-view">
        {{ design?.name }}
    </ViewMargin>
</template>

<style>

</style>
