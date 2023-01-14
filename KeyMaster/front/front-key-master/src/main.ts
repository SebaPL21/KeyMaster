import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import vuetify from './plugins/vuetify'
import { loadFonts } from './plugins/webfontloader'
import VueAxios from 'vue-axios'
import axios from 'axios'
import { createAuth0 } from '@auth0/auth0-vue'

loadFonts()

createApp(App)
  .use(router)
  .use(store)
  .use(vuetify)
  .use(VueAxios, axios)
  .use(
    createAuth0({
      domain: 'dev-zaydyt514u56uaza.us.auth0.com',
      client_id: '8HAoAHzIgTK333KUCdVgPTDyG7mYqtO9',
      redirect_uri: 'https://localhost:5002/profile'
    })
  )
  .mount('#app')
