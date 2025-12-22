<script setup lang="ts">
import ViewMargin from "@/components/layout/ViewMargin.vue";
import ThemeToggle from "@/components/ThemeToggle.vue"
import { Button } from "@/components/ui/button"
</script>

<template>
    <ViewMargin>
        <Button variant="secondary"> Test </Button>

        <ThemeToggle />
    </ViewMargin>
</template>
