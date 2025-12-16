import { register, init, getLocaleFromNavigator } from 'svelte-i18n';

register('en', () => import('../locales/en.json'));
register('de', () => import('../locales/de.json'));

export async function setupI18n() {
  await init({
    initialLocale: getLocaleFromNavigator(), 
    fallbackLocale: 'en',
  });
}
