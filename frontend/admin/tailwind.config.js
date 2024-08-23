// eslint-disable-next-line no-undef
module.exports = {
    darkMode: 'class',
    content: ['./index.html', './src/**/*.{js,jsx,ts,tsx}'],
    theme: {
        container: {
            center: true,
            padding: {
                DEFAULT: '1rem',
                sm: '2rem',
                lg: '4rem',
                xl: '5rem',
                '2xl': '6rem'
            }
        },
        extend: {
            backgroundColor: {
                'main-hover': '#0000000f',

                'main-color-1': '#131629',
                'main-color-2': '#161d40',
                'main-color-3': '#1c2755',
                'main-color-4': '#203175',
                'main-color-5': '#263ea0',
                'main-color-6': '#2b4acb',
                'main-color-7': '#5273e0',
                'main-color-8': '#7f9ef3',
                'main-color-9': '#a8c1f8',
                'main-color-10': '#d2e0fa'
            },
            boxShadow: {
                main: '0px 0px 10px 0px rgba(0, 0, 0, 0.02)',
                'main-inner': 'inset 0 2px 10px 0 rgb(0 0 0 / 0.02)'
            }
        }
    },
    plugins: []
};
