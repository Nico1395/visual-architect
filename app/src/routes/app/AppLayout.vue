<template>
    <div class="layout-header">
        <nav class="layout-navbar"></nav>
    </div>

    <div class="layout-content"><slot /></div>
</template>

<style>
html,
body,
#app {
    margin: 0;
    height: 100%;
}

body {
    font-family: "Inter", sans-serif;
}

#app {
    display: flex;
    flex-direction: column;
    gap: 1rem;
    padding: 1rem;
    box-sizing: border-box;
}

.layout-header {
    flex: none;

    .layout-navbar {
        border: 1px solid black;
        height: 60px;
    }
}

.layout-content {
    flex: 1;
}
</style>
