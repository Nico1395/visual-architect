import { register, init, getLocaleFromNavigator } from 'svelte-i18n';
import { browser } from '$app/environment';

register('en', () => import('../locales/en.json'));
register('de', () => import('../locales/de.json'));

export async function setupI18n() {
  await init({
    initialLocale: getLocaleFromNavigator(), 
    fallbackLocale: 'en',
  });
}
