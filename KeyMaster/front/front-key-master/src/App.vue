<template>
  <v-app>
    <v-main>
      <router-view/>
    </v-main>
  </v-app>
</template>

<script lang="ts">
import { defineComponent } from 'vue'
import NavbarVue from './components/Navbar.vue';
import ContentVue from './components/Content.vue';

export default defineComponent({
  name: 'App',
  components: {
    NavbarVue,ContentVue,
  },
  data () {
    return {
      //
    }
  },
})
</script>
