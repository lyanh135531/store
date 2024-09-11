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
                'main-fill-tertiary': 'rgba(0, 0, 0, 0.04)',

                'dark-main-primary': '#171717',
                'dark-main-surface-primary': '#212121',
                'dark-main-surface-secondary': '#2f2f2f',
                'dark-main-fill-tertiary': 'rgba(255, 255, 255, 0.08)'
            },
            textColor: {
                'main-primary': 'rgba(0, 0, 0, 0.88)',
                'main-secondary': 'rgba(0, 0, 0, 0.65)',
                'main-tertiary': 'rgba(0, 0, 0, 0.45)',
                'main-quaternary': 'rgba(0, 0, 0, 0.25)',
                'main-color-primary': '#0c8e9c',

                'dark-main-primary': 'rgba(255, 255, 255, 0.85)',
                'dark-main-secondary': 'rgba(255, 255, 255, 0.65)',
                'dark-main-tertiary': 'rgba(255, 255, 255, 0.45)',
                'dark-main-quaternary': 'rgba(255, 255, 255, 0.25)'
            },
            boxShadow: {
                main: '0px 0px 10px 0px rgba(0, 0, 0, 0.02)',
                primary: '0px 0px 10px 0px rgba(0, 0, 0, 0.1)',
                'main-inner': 'inset 0 2px 10px 0 rgb(0 0 0 / 0.02)'
            }
        }
    },
    plugins: []
};
