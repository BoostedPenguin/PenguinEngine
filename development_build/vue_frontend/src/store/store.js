import Vue from 'vue'
import Vuex from 'vuex'
import axios from 'axios'

Vue.use(Vuex)
const host = process.env.VUE_APP_BASE_BACKEND_ROOT

export default new Vuex.Store({
  state: {
    suggestions: {
    },
    searchItem: {},
  },
  mutations: {
    SET_Suggestions(state, payload) {
      state.suggestions = payload
    },
    SET_SearchItem(state, payload) {
      state.searchItem = payload
    },
  },
  getters: {
    GET_SearchItem: state => {
      return state.searchItem
    },
    getCurrentSuggestions: state => {
      return state.suggestions
    },
    getSuggestion: state => (guid) => {
      return state.suggestions.data.find(sug => sug.guid = guid)
    }
  },
  actions: {
    loadSuggestions({ commit, getters }) {
      console.log(getters.token)

      if (!getters.token) {
        return console.log("No token provided")
      }
      axios
        .get(`${host}/search/suggestions`, {
          headers: {
            Authorization: `Bearer ${getters.token}`, // send the access token through the 'Authorization' header
          },
        })
        .then(data => data.data)
        .then(items => {
          console.log(items)
          commit('SET_Items', ...items)
        })
    }
  }
})
