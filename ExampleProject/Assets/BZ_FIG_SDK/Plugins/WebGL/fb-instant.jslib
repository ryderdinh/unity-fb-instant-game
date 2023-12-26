let lib = {
$rewardAd: null,
$interstitialAd: null,
$wrapHooks: {
	callSendMessage: (objectName, methodName, value) => {
		if (!gameInstance) return
		if (value) {
			gameInstance.SendMessage(objectName, methodName, value)
		} else {
			gameInstance.SendMessage(objectName, methodName)
		}
	},
	convertString: str => {
		var bufferSize = lengthBytesUTF8(str) + 1
		var buffer = _malloc(bufferSize)
		stringToUTF8(str, buffer, bufferSize)
		return buffer
	},
	getContextSourceID: () => {
		let currentContextID = String(
			new URLSearchParams(window.location.search).get('context_source_id')
		)
		return currentContextID
	},
	createTournamentServer: (id, title, startTime, endTime, score) => {
		const start_at = new Date(Number(startTime)).toISOString()
		const end_at = new Date(Number(endTime)).toISOString()

		var headers = new Headers()
		headers.append('Content-Type', 'application/json')
		fetch('...', {
			method: 'POST',
			body: JSON.stringify({
				id_tour: id,
				tour_name: title,
				start_at,
				end_at,
				is_current: false
			}),
			redirect: 'follow',
			headers
		})
			.then(response => response.text())
			.then(() => {
				wrapHooks.updateUserScoreTournamentServer(score)
			})
			.catch(error => console.log('error', error))
	},
	updateUserScoreTournamentServer: score => {
		var headers = new Headers()
		headers.append('Content-Type', 'application/json')
		fetch(
			'...',
			{
				method: 'POST',
				body: JSON.stringify({
					id_in_tour: FBInstant.player.getID(),
					score_in_tour: score,
					tournament: FBInstant.context.getID()
				}),
				redirect: 'follow',
				headers
			}
		)
			.then(response => response.text())
			.then(result => console.log(result))
			.catch(error => console.log('error', error))
	}
},
WebGLWindowInit: function () {
	// Remove the `Runtime` object from "v1.37.27: 12/24/2017"
	// if Runtime not defined. create and add functon!!
	if (typeof Runtime === 'undefined') Runtime = { dynCall: dynCall }
},
startFbGame: function () {
	FBInstant.startGameAsync()
},
getFbName: function () {
	const result = FBInstant.player.getName() || ''
	var bufferSize = lengthBytesUTF8(result) + 1
	var buffer = _malloc(bufferSize)
	stringToUTF8(result, buffer, bufferSize)
	return buffer
},
getFbAvatar: function () {
	const result = FBInstant.player.getPhoto() || ''
	var bufferSize = lengthBytesUTF8(result) + 1
	var buffer = _malloc(bufferSize)
	stringToUTF8(result, buffer, bufferSize)
	return buffer
},
getFbId: function () {
	const result = FBInstant.player.getID() || 'test'
	var bufferSize = lengthBytesUTF8(result) + 1
	var buffer = _malloc(bufferSize)
	stringToUTF8(result, buffer, bufferSize)
	return buffer
},
preloadRewardedAd: function (placementId) {
	if (!rewardAd) {
		FBInstant.getRewardedVideoAsync(UTF8ToString(placementId))
			.then(ad_instance => {
				console.log('Preload rewarded ads: ', ad_instance)
				rewardAd = ad_instance
				return rewardAd.loadAsync()
			})
			.catch(err => console.log('Preload rewarded ads: ', err))
	}
},
showRewardedAd: function (placementId) {
	console.log('Reward Ads: ', rewardAd)
	if (rewardAd) {
		rewardAd
			.showAsync()
			.then(() => {
				SendMessage('WrapManager', 'onRewardedAdSuccess')
				rewardAd = null
			})
			.catch(err => {
				if (typeof err == 'object' && 'code' in err) {
					SendMessage('WrapManager', 'onRewardedAdFailed', err.code)
				}
				console.log(err)
				rewardAd = null
			})
	} else {
		let rewardAdInstance = null
		FBInstant.getRewardedVideoAsync(UTF8ToString(placementId))
			.then(ad_instance => {
				rewardAdInstance = ad_instance
				console.log('Preload rewarded ads: ', rewardAdInstance)
				return rewardAdInstance.loadAsync()
			})
			.then(function () {
				console.log('Show rewarded ads', rewardAdInstance)
				return rewardAdInstance.showAsync()
			})
			.then(() => SendMessage('WrapManager', 'onRewardedAdSuccess'))
			.catch(err => {
				if (typeof err == 'object' && 'code' in err) {
					SendMessage('WrapManager', 'onRewardedAdFailed', err.code)
				}
				console.log('Reward ads error: ', err)
			})
	}
},
preloadInterstitialAd: function (placementId) {
	if (!interstitialAd) {
		FBInstant.getInterstitialAdAsync(UTF8ToString(placementId))
			.then(async ad_instance => {
				console.log('Preloaded Inter Ads')
				interstitialAd = ad_instance
				return interstitialAd.loadAsync()
			})
			.catch(err => {
				console.log('preloadInterstitialAdErr:')
				console.error(err)
			})
	}
},
showInterstitialAd: function (placementId) {
	console.log('InterstitialAd: ', interstitialAd)
	if (interstitialAd) {
		interstitialAd
			.showAsync()
			.then(() => {
				SendMessage('WrapManager', 'onInterstitialAdSuccess')
				interstitialAd = null
			})
			.catch(() => {
				console.log('showInterstitialAdErr:')
				console.error(err)
				SendMessage('WrapManager', 'onInterstitialAdFailed')
			})
	} else {
		let interstitialAdInstance = null
		FBInstant.getInterstitialAdAsync(UTF8ToString(placementId))
			.then(ad_instance => {
				interstitialAdInstance = ad_instance
				console.log('Preloaded Inter Ads', interstitialAdInstance)
				return interstitialAdInstance.loadAsync()
			})
			.then(function () {
				console.log('---->showInterstitialAd: ', interstitialAdInstance)
				return interstitialAdInstance.showAsync()
			})
			.then(() => SendMessage('WrapManager', 'onInterstitialAdSuccess'))
			.catch(err => {
				SendMessage('WrapManager', 'onInterstitialAdFailed')
				console.log('InterstitialAdErr:')
				console.error(err)
			})
	}
},
share: function (image, text, switchContext = false) {
	const shareDestination = ['NEWSFEED', 'GROUP', 'COPY_LINK', 'MESSENGER']
	FBInstant.shareAsync({
		image: UTF8ToString(image),
		text: UTF8ToString(text),
		data: {},
		shareDestination: shareDestination,
		switchContext: false
	})
		.then(function () {
			SendMessage('WrapManager', 'onShareSuccess')
		})
		.catch(function () {})
},
loadBannerAds: function (placementId) {
	FBInstant.loadBannerAdAsync(UTF8ToString(placementId), 'bottom')
		.then(() => {
			console.log('Loaded banner ads')
			SendMessage('WrapManager', 'OnLoadedBannerAds')
		})
		.catch(err => console.error(err))
},
hideBannerAds: function () {
	FBInstant.hideBannerAdAsync()
},
setUserData: function (type, name, value, isFlush) {
	let convertValue = UTF8ToString(value)
	console.log('User data input: ', UTF8ToString(name), convertValue)
	switch (type) {
		case 'int':
			convertValue = Number(convertValue)
			break

		default:
			break
	}

	const dataName = UTF8ToString(name)

	FBInstant.player
		.setDataAsync({ [dataName]: convertValue })
		.then(() => {
			if (isFlush === 1) {
				FBInstant.player.flushDataAsync()
			}
		})
		.then(() => {
			console.log('User data updated: ', dataName, convertValue)
		})
		.catch(err => {
			console.error(err)
		})
},
inviteFriend: function (imageText, jsonData = '{}') {
	let image = UTF8ToString(imageText)
	let data = JSON.parse(UTF8ToString(jsonData))
	window.wrap.inviteFriend(image, data)
},
createTournament: function (initialScore) {
	console.log('Creating tournament: Pending user')
	FBInstant.tournament
		.createAsync({
			initialScore,
			config: {
				title: 'New tournament waiting for u. Join now!',
				sortOrder: 'HIGHER_IS_BETTER',
				scoreFormat: 'NUMERIC'
			}
		})
		.then(tournament => {
			console.log('Tournament created: ', tournament)
			SendMessage('WrapManager', 'onCreateTournamentSuccess')

			const current = new Date()

			wrapHooks.createTournamentServer(
				tournament.getID(),
				tournament.getTitle(),
				current.getTime(),
				tournament.getEndTime(),
				initialScore
			)
		})
		.catch(err => {
			console.log('Create tournament failed')
			console.error(err)
		})
},
postTournamentScore: function (score, HIGHER_IS_BETTER = 1) {
	const currentTournamentID = FBInstant.context.getID()

	FBInstant.player
		.getDataAsync(['tournament'])
		.then(data => {
			const tourData = data || {}
			const lastScore = data[currentTournamentID] || 100000000000

			const condition =
				HIGHER_IS_BETTER === 1 ? lastScore < score : lastScore > score

			if (condition) {
				FBInstant.tournament
					.postScoreAsync(score)
					.then(function () {
						console.log(`Post new highscore (${score}) successfully!`)

						tourData[currentTournamentID] = score

						FBInstant.player
							.setDataAsync({
								tournament: tourData
							})
							.then(() => {
								console.log(`Updated new highscore (${score}) in user data`)
							})
							.catch(err => {
								console.log(
									`Updated failed new highscore (${score}) in user data`
								)
								console.error(err)
							})

						FBInstant.tournament
							.shareAsync({
								score
							})
							.then(function () {
								console.log(`Shared new highscore!`)
							})

						wrapHooks.updateUserScoreTournamentServer(score)
					})
					.catch(err => {
						console.log('Post new highscore failed!')
						console.error(err)
					})
			}
		})
		.catch(err => console.error(err))
},
shareTournament: function (score) {
	FBInstant.tournament
		.shareAsync({
			score
		})
		.then(function () {
			console.log(`Shared new highscore!`)
		})
},
openFollowOfficialPage: function () {
	FBInstant.community.canFollowOfficialPageAsync().then(() => {
		FBInstant.community.followOfficialPageAsync()
		window.localStorage.setItem(
			productName,
			Number(window.localStorage.getItem(productName) || 0) + 1
		)
	})
},
updateUserDataToCloud: function (stringUpdateData) {
	const data = UTF8ToString(stringUpdateData)
	window.wrap.updateUserDataToCloud(data)
},
getFacebookFriend: function (onSuccess) {
	// FBInstant.graphApi
	// 	.requestAsync('/me?fields=friends')
	// 	.then(function (response) {
	// 		if (response && 'friends' in response) {
	// 			const friendsJsonString = JSON.stringify(response.friends)
	// 			Runtime.dynCall('vi', onSuccess, [
	// 				wrapHooks.convertString(friendsJsonString)
	// 			])
	// 		}
	// 	})

	FBInstant.player.getConnectedPlayersAsync().then(function (players) {
		const friendsJsonString = JSON.stringify({ players: players })
		Runtime.dynCall('vi', onSuccess, [
			wrapHooks.convertString(friendsJsonString)
		])
	})
},
switchAsync: function (contextId) {
	FBInstant.context.switchAsync(UTF8ToString(contextId)).then(function () {
		SendMessage('WrapManager', 'OnSwitchAsyncSuccess')
	})
},
fetchCurrentContextData: function (
	openTournamentPopup = 0,
	delayOpenTournamentPopup = 1500
) {
	// openTournamentPopup value: 0 or 1 (false or true)
	// delayOpenTournamentPopup value: milisecond

	handleCurrentContext(openTournamentPopup === 1, delayOpenTournamentPopup)
}
}

autoAddDeps(lib, '$rewardAd')
autoAddDeps(lib, '$interstitialAd')
autoAddDeps(lib, '$wrapHooks')
mergeInto(LibraryManager.library, lib)
