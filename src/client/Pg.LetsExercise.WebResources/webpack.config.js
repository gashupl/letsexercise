const path = require("path");
const { CleanWebpackPlugin } = require("clean-webpack-plugin");
const CopyWebpackPlugin = require('copy-webpack-plugin');

module.exports = {
    devtool: "source-map",
    entry: {
        Main: "./src/Main", 
        goalsmonthlychart: "./src/pages/goalsmonthlychart.ts"
    },
    output: {
        filename: "scripts/[name].js", // Use [name] to dynamically name the output files
        sourceMapFilename: "scripts/maps/[name].js.map", // Use [name] for source maps
        path: path.resolve(__dirname, "./Webresources"),
        library: ["LetsExercise"],
        libraryTarget: "var",
    },
    module: {
        rules: [
            {
                test: /\.(ts|tsx)$/,
                use: "ts-loader",
                exclude: /node_modules/,
            },
        ],
    },
    plugins: [
        new CleanWebpackPlugin(), 
        new CopyWebpackPlugin({
            patterns: [
                { from: 'html/goalsmonthlychart.html', to: 'pages/' }
            ]
        })
    ],
    resolve: {
        extensions: [".ts", ".js"],
    },
};

