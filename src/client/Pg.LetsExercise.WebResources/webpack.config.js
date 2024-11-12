const path = require("path");
const { CleanWebpackPlugin } = require("clean-webpack-plugin");
module.exports = {
    devtool: "source-map",
    entry: {
        Main: "./src/Main", 
        GoalsMonthlyChart: "./src/pages/goalsmonthlychart.ts"
    },
    output: {
        filename: "[name].js", // Use [name] to dynamically name the output files
        sourceMapFilename: "maps/[name].js.map", // Use [name] for source maps
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

