import Vue from 'vue'
import Vuex from 'vuex'
import axios from 'axios'

Vue.use(Vuex)
const host = process.env.VUE_APP_BASE_BACKEND_ROOT

export default new Vuex.Store({
  state: {
    user_role: {},
    trip: {},
    suggestions: {},
    wishlist: {},
    searchItem: {},
    userTrips: {},
    supportTickets: {},
    base_url: process.env.VUE_APP_BASE_BACKEND_ROOT,
    google_key: process.env.VUE_APP_GOOGLE_KEY,
    ip_stack: process.env.VUE_APP_IP_STACK_KEY,
  },
  mutations: {
    SET_RemoveUserSpecificInformation(state) {
      state.user_role = null
      state.trip = null
      state.suggestions = null
      state.wishlist = null
      state.searchItem = null
      state.userTrips = null
      state.supportTickets = null
    },
    SET_Suggestions(state, payload) {
      state.suggestions = payload
    },
    SET_SearchItem(state, payload) {
      state.searchItem = payload
    },
    SET_SupportTicket(state, payload) {
      state.supportTickets = payload
    },
    SET_WishlistItems(state, payload) {
      state.wishlist = payload
    },
    SET_SelectedTrip(state, payload) {
      state.trip = payload
    },
    SET_UserRole(state, payload) {
      state.user_role = payload
    },
    SET_UserTrips(state, payload) {
      state.userTrips = payload
    },
    SET_SearchItemInWishlist(state, payload) {
      if (state.searchItem.placeId == 0 || payload.placeId == 0) return
      if (state.searchItem.placeId == payload.placeId || payload.placeId == null) {
        state.searchItem.alreadyInWishlist = payload.isAlreadyInWishlist
      }
    }
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
        return console.log("Fuck my life")
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
    },
    // async Testing(context, payload) {
    //   axios
    //     .get(`${context.state.base_url}/wishlist`, {
    //       headers: {
    //         Authorization: `Bearer ${payload.authToken}`, // send the access token through the 'Authorization' header
    //       },
    //     })
    //     .then((data) => {
    //       context.commit("SET_WishlistItems", data.data)
    //       console.log(data)
    //     })
    //     .catch((err) => (this.error = err))
    // },
  },
})
