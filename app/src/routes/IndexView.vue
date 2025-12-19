<script setup lang="ts">
import { http } from "@/http";
import { onMounted } from "vue";
import { useRouter } from "vue-router";

const router = useRouter();

onMounted(async () => {
  try {
    await http.get("/api/auth/me");

    // optional später:
    //   const { data } = await http.get("/auth/me");
    // if (data.isFirstLogin) router.replace("/welcome");

    router.replace("/app/home");
  } catch {
    router.replace("/auth/login");
  }
});
</script>

<template>loading</template>
