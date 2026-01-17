<template>
    <div class="about-section">
        <div class="about-section-header">
            <slot name="header" />
        </div>

        <div class="about-section-body">
            <slot name="body" />
        </div>
    </div>
</template>

<style>
.about-section {
    flex: none;
    display: flex;
    flex-direction: column;
    gap: 1rem;

    .about-section-header {
        font-weight: 700;
        font-size: 20pt;
    }

    .about-section-body {
        font-size: 11pt;

        p {
            margin-bottom: 0.5rem;
        }
    }
}
</style>
