<template>
  <NavbarVue></NavbarVue>
  <v-container>
    <div class="test-title">
      <h1>
        Witaj na stronie testowej.
      </h1>
      <p>
        Tutaj będziesz mógł/mogła sprawdzić swoje postępy w praktycznym teście.
      </p>
    </div>
    <Content></Content>
  </v-container>
  <FooterVue></FooterVue>
</template>

<script lang='ts'>
import { defineComponent } from 'vue'
import NavbarVue from '../components/Navbar.vue';
import Content from '@/components/Content.vue';
import FooterVue from '../components/Footer.vue';

export default defineComponent({
  name: 'HelloWorld',
  components: {
    NavbarVue,
    FooterVue,
    Content
  },

})
</script>