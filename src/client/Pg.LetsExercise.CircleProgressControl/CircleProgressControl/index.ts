import { IInputs, IOutputs } from "./generated/ManifestTypes";
import { CircleProgress, ICircleProgressProps } from "./CircleProgress";
import * as React from "react";

export class CircleProgressControl implements ComponentFramework.ReactControl<IInputs, IOutputs> {
    private theComponent: ComponentFramework.ReactControl<IInputs, IOutputs>;
    private notifyOutputChanged: () => void;
    private percentage: number;

    constructor() { }

    public init(
        context: ComponentFramework.Context<IInputs>,
        notifyOutputChanged: () => void,
        state: ComponentFramework.Dictionary
    ): void {
        this.notifyOutputChanged = notifyOutputChanged;
    }

    public updateView(context: ComponentFramework.Context<IInputs>): React.ReactElement {       
        this.percentage = context.parameters.percentageCompleted.raw || 0;
        const props: ICircleProgressProps = { percentage: this.percentage };
        return React.createElement(
            CircleProgress, props
        );
    }

    public getOutputs(): IOutputs {
        return { };
    }

    public destroy(): void {
        // Add code to cleanup control if necessary
    }
}
