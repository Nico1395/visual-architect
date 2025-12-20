<script setup lang="ts">
import http from "@/http";
import { onMounted, ref } from "vue";
import { useRouter } from "vue-router";

const router = useRouter();
const csrfToken = ref<string | null>(null);

onMounted(async () => {
  try {
    await http.get("/api/auth/me");

    // optional später:
    //   const { data } = await http.get("/auth/me");
    // if (data.isFirstLogin) router.replace("/welcome");

    const { data } = await http.get("/api/auth/csrf");
    csrfToken.value = data.token;
    http.defaults.headers.common["X-CSRF-TOKEN"] = csrfToken.value;

    router.replace("/app/home");
  } catch {
    router.replace("/auth/login");
  }
});
</script>

<template>loading</template>
