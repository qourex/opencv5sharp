import { defineConfig } from 'vitepress'

export default defineConfig({
  base: '/opencv5sharp/',
  title: 'OpenCV5Sharp',
  description: 'High-Performance OpenCV 5 Wrapper for .NET',
  cleanUrls: true,
  lastUpdated: true,
  sitemap: {
    hostname: 'https://qourex.github.io/opencv5sharp/'
  },
  head: [
    ['link', { rel: 'icon', type: 'image/png', href: 'https://raw.githubusercontent.com/qourex/opencv5sharp/main/src/OpenCV5Sharp/icon.png' }],
    ['meta', { name: 'author', content: 'Qourex' }],
    ['meta', { name: 'keywords', content: 'opencv, opencv5, computer-vision, image-processing, dotnet, csharp, cuda, gpu, gpu-acceleration, android, ios, maui, winforms, aspnetcore, blazor' }],
    ['meta', { property: 'og:title', content: 'OpenCV5Sharp' }],
    ['meta', { property: 'og:description', content: 'High-performance C# SDK wrapping OpenCV 5 with CUDA/cuDNN GPU acceleration in .NET.' }],
    ['meta', { property: 'og:image', content: 'https://raw.githubusercontent.com/qourex/opencv5sharp/main/social_card.png' }],
    ['meta', { property: 'og:type', content: 'website' }],
    ['meta', { name: 'twitter:card', content: 'summary_large_image' }],
    ['meta', { name: 'twitter:title', content: 'OpenCV5Sharp' }],
    ['meta', { name: 'twitter:description', content: 'High-performance C# SDK wrapping OpenCV 5 with CUDA/cuDNN GPU acceleration in .NET.' }],
    ['meta', { name: 'twitter:image', content: 'https://raw.githubusercontent.com/qourex/opencv5sharp/main/social_card.png' }],
    [
      'script',
      { type: 'application/ld+json' },
      JSON.stringify({
        '@context': 'https://schema.org',
        '@type': 'SoftwareApplication',
        'name': 'OpenCV5Sharp',
        'operatingSystem': 'Windows, Linux, macOS, Android, iOS',
        'applicationCategory': 'DeveloperApplication',
        'description': 'High-performance C# wrapper for OpenCV 5 with GPU/CUDA acceleration for .NET.',
        'offers': {
          '@type': 'Offer',
          'price': '0',
          'priceCurrency': 'USD'
        },
        'author': {
          '@type': 'Organization',
          'name': 'Qourex',
          'url': 'https://qourex.com'
        },
        'downloadUrl': 'https://www.nuget.org/packages/OpenCV5Sharp',
        'softwareVersion': process.env.PACKAGE_VERSION || '1.0.0',
        'license': 'https://opensource.org/licenses/Apache-2.0'
      })
    ]
  ],
  markdown: {
    math: true
  },
  themeConfig: {
    logo: 'https://raw.githubusercontent.com/qourex/opencv5sharp/main/src/OpenCV5Sharp/icon.png',
    nav: [
      { text: 'Guide', link: '/guide/getting-started' },
      { text: 'Advanced', link: '/guide/advanced-features' },
      { text: 'API Reference', link: '/guide/api-reference' },
      { text: 'Samples', link: '/guide/samples' },
      { text: 'Mobile Deployment', link: '/guide/mobile-deployment' },
      { text: 'NuGet', link: 'https://www.nuget.org/packages/OpenCV5Sharp' }
    ],
    sidebar: [
      {
        text: 'Introduction',
        items: [
          { text: 'Getting Started', link: '/guide/getting-started' }
        ]
      },
      {
        text: 'Features & API',
        items: [
          { text: 'Advanced Usage', link: '/guide/advanced-features' },
          { text: 'API Reference', link: '/guide/api-reference' }
        ]
      },
      {
        text: 'API Module References',
        items: [
          { text: 'Core Module', link: '/guide/reference/core' },
          { text: 'Imgproc (Image Processing)', link: '/guide/reference/imgproc' },
          { text: 'Dnn (Deep Learning)', link: '/guide/reference/dnn' },
          { text: 'Objdetect (Object Detection)', link: '/guide/reference/objdetect' },
          { text: 'Imgcodecs (Image Codecs)', link: '/guide/reference/imgcodecs' },
          { text: 'Features (Feature Detection)', link: '/guide/reference/features' },
          { text: 'Video (Motion & Tracking)', link: '/guide/reference/video' },
          { text: 'VideoIO (Video Input/Output)', link: '/guide/reference/videoio' },
          { text: 'Ptcloud (Point Cloud)', link: '/guide/reference/ptcloud' },
          { text: 'Stereo (Stereo)', link: '/guide/reference/stereo' },
          { text: 'Stitching (Image Stitching)', link: '/guide/reference/stitching' },
          { text: 'Calib (Camera Calibration)', link: '/guide/reference/calib' },
          { text: 'Photo (Photography)', link: '/guide/reference/photo' },
          { text: 'Geometry (Geometry)', link: '/guide/reference/geometry' },
          { text: 'Flann (Nearest Neighbors)', link: '/guide/reference/flann' },
          { text: 'Highgui (GUI / Display)', link: '/guide/reference/highgui' }
        ]
      },
      {
        text: 'Samples & Demos',
        items: [
          { text: 'The .NET Suite', link: '/guide/samples' }
        ]
      },
      {
        text: 'Platform Support',
        items: [
          { text: 'Mobile Deployment (Android & iOS)', link: '/guide/mobile-deployment' }
        ]
      },
      {
        text: 'Maintenance',
        items: [
          { text: 'Package Signing', link: '/SIGNING' }
        ]
      }
    ],
    socialLinks: [
      { icon: 'github', link: 'https://github.com/qourex/opencv5sharp' }
    ],
    footer: {
      message: 'Released under the Apache 2.0 and LGPL 2.1 Licenses.',
      copyright: 'Copyright © 2026 Qourex'
    },
    search: {
      provider: 'local'
    }
  }
})
