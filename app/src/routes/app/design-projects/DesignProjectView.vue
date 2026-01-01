<script setup lang="ts">
import { useRoute } from "vue-router"

const route = useRoute()
const projectId = route.params.projectId as string
</script>

<template>
    <h1>
        {{ projectId }}
    </h1>
</template>

<style>

</style>
