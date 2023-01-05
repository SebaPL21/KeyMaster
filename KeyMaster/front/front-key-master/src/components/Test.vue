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
    <div class="containertmp">
      <div class="text-lesson">
        Lesson container
      </div>    
  </div>
</v-container>
<FooterVue></FooterVue>
</template>

<script lang='ts'>
import { defineComponent } from 'vue'
import NavbarVue from './Navbar.vue';
import FooterVue from './Footer.vue';

export default defineComponent({
  name: 'HelloWorld',
  components: {
    NavbarVue,FooterVue,
  },

  data () {
    return {
      ecosystem: [
        {
          text: 'vuetify-loader',
          href: 'https://github.com/vuetifyjs/vuetify-loader/tree/next',
        },
        {
          text: 'github',
          href: 'https://github.com/vuetifyjs/vuetify/tree/next',
        },
        {
          text: 'awesome-vuetify',
          href: 'https://github.com/vuetifyjs/awesome-vuetify',
        },
      ],
      importantLinks: [
        {
          text: 'Chat',
          href: 'https://community.vuetifyjs.com',
        },
        {
          text: 'Made with Vuetify',
          href: 'https://madewithvuejs.com/vuetify',
        },
        {
          text: 'Twitter',
          href: 'https://twitter.com/vuetifyjs',
        },
        {
          text: 'Articles',
          href: 'https://medium.com/vuetify',
        },
      ],
      whatsNext: [
        {
          text: 'Explore components',
          href: 'https://vuetifyjs.com',
        },
        {
          text: 'Roadmap',
          href: 'https://vuetifyjs.com/en/introduction/roadmap/',
        },
        {
          text: 'Frequently Asked Questions',
          href: 'https://vuetifyjs.com/getting-started/frequently-asked-questions',
        },
      ],
    }
  },
})
</script>
