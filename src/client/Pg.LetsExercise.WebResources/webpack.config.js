const path = require("path");
const { CleanWebpackPlugin } = require("clean-webpack-plugin");
module.exports = {
    devtool: "source-map",
    entry: {
        Main: "./src/Main"
    },
    output: {
        filename: "letsexercise-main.js",
        sourceMapFilename: "maps/letsexercise-main.js.map",
        path: path.resolve(__dirname, "./Webresources/scripts"),
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
    plugins: [new CleanWebpackPlugin()],
    resolve: {
        extensions: [".ts", ".js"],
    },
};

