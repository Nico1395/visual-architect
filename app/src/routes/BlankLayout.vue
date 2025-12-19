<template>
    <slot />
</template>

<style>
html,
body {
    margin: 0;
    height: 100%;
}

body {
    font-family: "Inter", sans-serif;
}

#app {
    margin: 0;
    height: 100%;
    display: flex;
    flex-direction: column;
    gap: 1rem;
    padding: 1rem;
    box-sizing: border-box;
}
</style>
