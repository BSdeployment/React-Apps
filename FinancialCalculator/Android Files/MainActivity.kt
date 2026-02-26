package com.example.newfinancialcalculator

import android.content.Intent
import android.net.Uri
import android.os.Bundle
import android.webkit.*
import androidx.activity.addCallback
import androidx.appcompat.app.AppCompatActivity
import androidx.webkit.WebViewAssetLoader

class MainActivity : AppCompatActivity() {

    private lateinit var webView: WebView
    private lateinit var assetLoader: WebViewAssetLoader

    private val internalHost = "app"

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        setContentView(R.layout.activity_main)

        webView = findViewById(R.id.webView)

        setupWebView()
        setupAssetLoader()
        setupBackHandler()

        webView.loadUrl("https://app/index.html")
    }

    private fun setupWebView() {

        val settings = webView.settings
        settings.javaScriptEnabled = true
        settings.domStorageEnabled = true
        settings.loadsImagesAutomatically = true
        settings.mediaPlaybackRequiresUserGesture = false

        WebView.setWebContentsDebuggingEnabled(true)

        webView.webChromeClient = WebChromeClient()

        webView.webViewClient = object : WebViewClient() {

            override fun shouldInterceptRequest(
                view: WebView,
                request: WebResourceRequest
            ): WebResourceResponse? {
                return assetLoader.shouldInterceptRequest(request.url)
            }

            override fun shouldOverrideUrlLoading(
                view: WebView,
                request: WebResourceRequest
            ): Boolean {

                val uri = request.url

                return if (isExternalUrl(uri)) {
                    openExternalBrowser(uri)
                    true
                } else {
                    false
                }
            }
        }
    }

    private fun setupAssetLoader() {
        assetLoader = WebViewAssetLoader.Builder()
            .setDomain(internalHost)
            .addPathHandler(
                "/",
                WebViewAssetLoader.AssetsPathHandler(this)
            )
            .build()
    }

    private fun setupBackHandler() {
        onBackPressedDispatcher.addCallback(this) {
            if (webView.canGoBack()) {
                webView.goBack()
            } else {
                finish()
            }
        }
    }

    private fun isExternalUrl(uri: Uri): Boolean {
        return uri.scheme?.startsWith("http") == true &&
                uri.host != internalHost
    }

    private fun openExternalBrowser(uri: Uri) {
        val intent = Intent(Intent.ACTION_VIEW, uri)
        startActivity(intent)
    }
}
