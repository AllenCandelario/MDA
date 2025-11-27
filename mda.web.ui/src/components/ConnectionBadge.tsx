import { useSignalRContext } from "../app/providers/SignalRProvider";

export default function ConnectionBadge() {
    const { isConnected } = useSignalRContext();

    return (

        <div className="fixed top-4 right-4 z-50">
            <span
                className={`inline-flex items-center rounded-full px-3 py-1 text-xs font-medium shadow ${
                isConnected ? "bg-green-100 text-green-800" : "bg-red-100 text-red-800"}`}
            >
                <span className={`mr-2 h-2 w-2 rounded-full ${isConnected ? "bg-green-500" : "bg-red-500"}`}/>
                {isConnected ? "Connected" : "Disconnected"}
            </span>
        </div>
    )
}