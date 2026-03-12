import { NextResponse } from "next/server";

export async function GET(req: Request) {
  const { searchParams } = new URL(req.url);
  const city = searchParams.get("city");

  const apiKey = process.env.WEATHER_API_KEY;

  const res = await fetch(
    `https://api.openweathermap.org/data/2.5/weather?q=${city}&appid=${apiKey}&units=metric`
  );

  if (!res.ok) {
    return NextResponse.json({ error: "City not found" }, { status: 404 });
  }

  const data = await res.json();

  return NextResponse.json({
    temperature: data.main.temp,
    humidity: data.main.humidity,
    description: data.weather[0].description,
  });
}